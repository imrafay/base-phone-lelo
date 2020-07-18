import { ProjectTemplatePage } from './app.po';

describe('Project App', function() {
  let page: ProjectTemplatePage;

  beforeEach(() => {
    page = new ProjectTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
