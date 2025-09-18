import { DashboardTemplatePage } from './app.po';

describe('Dashboard App', function () {
    let page: DashboardTemplatePage;

    beforeEach(() => {
        page = new DashboardTemplatePage();
    });

    it('should display message saying app works', () => {
        page.navigateTo();
        expect(page.getParagraphText()).toEqual('app works!');
    });
});
