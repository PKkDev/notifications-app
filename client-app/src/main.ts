import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

if (environment.production) {
  enableProdMode();
}

export function getProdUrl() {
  const baseUrl = document.getElementsByTagName('base')[0].href;
  return baseUrl + 'api/'
}

export function getBaseUrl() {
  if (environment.production) {
    return getProdUrl();
  } else {
    // return getProdUrl();
    // return 'https://skillboxwebserviceapi.azurewebsites.net/api/';
    return 'https://localhost:44357/api/';
    // return 'http://localhost:5000/api/';
  }
}

const providers = [
  {
    provide: 'BASE_APP_URL',
    useFactory: getBaseUrl,
    deps: []
  }
];

platformBrowserDynamic(providers).bootstrapModule(AppModule)
  .catch(err => console.error(err));
