import { Component, Injector, ChangeDetectionStrategy, OnInit, OnDestroy } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { LocalizePipe } from '@shared/pipes/localize.pipe';
import { Subscription } from 'rxjs';
import { Chart, registerables } from 'chart.js';
import { TelemetryServiceProxy, SensorReadingDto, AggregatedStatsDto } from '@shared/service-proxies/service-proxies';
import { CommonModule } from '@angular/common';
import { TelemetryService } from './telemetry.service';
Chart.register(...registerables);

@Component({
    templateUrl: './home.component.html',
    styleUrl: './home.component.css',
    animations: [appModuleAnimation()],
    changeDetection: ChangeDetectionStrategy.OnPush,
    standalone: true,
    imports: [LocalizePipe, CommonModule],
})
export class HomeComponent extends AppComponentBase implements OnInit, OnDestroy {
    readings: SensorReadingDto[] = [];
    stats: AggregatedStatsDto = new AggregatedStatsDto();

    subs: Subscription[] = [];
    chart: Chart;
    constructor(
        injector: Injector,
        private telemetryService: TelemetryService,
        private telemetryProxy: TelemetryServiceProxy
    ) {
        super(injector);
    }
    ngOnInit(): void {
        // Connect SignalR
        this.telemetryService.connect();
        // Load initial readings from backend (DB)
        this.telemetryProxy.getRecent(1000).subscribe((data) => {
            this.readings = data;
            this.renderChart(data);
        });

        // Subscribe to live updates
        this.subs.push(
            this.telemetryService.getReadings$().subscribe((readings) => {
                if (!readings || readings.length === 0) {
                    return;
                }
                const latest = readings[readings.length - 1];
                if (!this.chart) {
                    this.initChart(); // empty chart setup
                }
                this.addReading(latest);
                this.telemetryProxy.getStats(5).subscribe((data) => {
                    this.stats.avg = data.avg;
                    this.stats.count = data.count;
                    this.stats.min = data.min;
                    this.stats.max = data.max;
                    console.log('DEBUG stats from API:', data);
                    console.log('DEBUG stats from API:', this.stats);
                });
            })
        );
    }

    ngOnDestroy(): void {
        this.subs.forEach((s) => s.unsubscribe());
        if (this.chart) {
            this.chart.destroy();
        }
    }
    isAnomaly(value: number): boolean {
        return value > this.stats.avg * 1.5 || value < this.stats.avg * 0.5;
    }

    private initChart() {
        this.chart = new Chart('telemetryChart', {
            type: 'line',
            data: {
                labels: [],
                datasets: [
                    {
                        label: 'Sensor Values',
                        data: [],
                        borderColor: 'blue',
                        tension: 0.1,
                    },
                ],
            },
        });
    }
    // ----- Chart.js handling -----
    private renderChart(data: SensorReadingDto[]) {
        if (!this.chart) {
            // create chart once
            this.chart = new Chart('telemetryChart', {
                type: 'line',
                data: {
                    labels: data.map((r) => new Date(r.timestamp).toLocaleTimeString()),
                    datasets: [
                        {
                            label: 'Sensor Values',
                            data: data.map((r) => r.value),
                            borderColor: 'green',
                            tension: 0.1,
                        },
                    ],
                },
            });
        } else {
            // update existing chart
            this.chart.data.labels = data.map((r) => new Date(r.timestamp).toLocaleTimeString());
            this.chart.data.datasets[0].data = data.map((r) => r.value);
            this.chart.update();
        }
    }

    private addReading(reading: SensorReadingDto) {
        const MAX_POINTS = 1000;

        const labels = this.chart.data.labels as string[];
        const data = this.chart.data.datasets[0].data as number[];

        labels.push(new Date(reading.timestamp).toLocaleTimeString());
        data.push(reading.value);

        if (labels.length > MAX_POINTS) {
            labels.shift();
            data.shift();
        }

        this.chart.update('none'); // skip animations for performance
    }
}
