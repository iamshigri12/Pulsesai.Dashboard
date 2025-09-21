import { Injectable, NgZone } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { SensorReadingDto, TelemetryServiceProxy } from '@shared/service-proxies/service-proxies';
import { environment } from '../../environments/environment';
@Injectable({ providedIn: 'root' })
export class TelemetryService {
    private hubConnection: signalR.HubConnection;
    private readings$ = new BehaviorSubject<SensorReadingDto[]>([]);
    // keep at most 10,000 readings client-side
    private maxBuffer = 10000;

    constructor(
        private zone: NgZone,
        private telemetryProxy: TelemetryServiceProxy
    ) {}

    // ----- SignalR connection -----
    connect(): void {
        this.hubConnection = new signalR.HubConnectionBuilder()
            .withUrl(environment.apiUrl + '/hubs/telemetry')
            .withAutomaticReconnect()
            .build();
        this.hubConnection.start();
        //this.hubConnection.start().catch(err => console.error('SignalR connection error:', err));

        // When backend pushes new readings
        this.hubConnection.on('ReceiveReadings', (batch: SensorReadingDto[]) => {
            this.zone.run(() => {
                const current = this.readings$.value.concat(batch);
                const limited = current.slice(-this.maxBuffer); // enforce buffer size
                this.readings$.next(limited);
            });
        });
    }
    // ----- Public APIs -----
    getReadings$() {
        return this.readings$.asObservable();
    }

    // Load historical readings from DB
    loadRecentFromDb(limit = 1000): void {
        this.telemetryProxy.getRecent(limit).subscribe((data) => {
            this.readings$.next(data);
        });
    }
}
