import { HttpClient, HttpHeaders, HttpParams, HttpRequest } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

export class ModelHttpOptions {
  headers?: HttpHeaders | {
    [header: string]: string | string[];
  };
  observe?: 'body';
  params?: HttpParams | {
    [param: string]: string | string[];
  };
  reportProgress?: boolean;
  responseType?: 'json';
  withCredentials?: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(
    private httpClient: HttpClient,
    @Inject('BASE_APP_URL') public urlApi: string) { }

  public get<T>(path: string, params?: HttpParams, headers?: HttpHeaders): Observable<T> {
    const httpOptions = this.getBasicHttpOptions();
    if (params)
      httpOptions.params = params;
    if (headers)
      httpOptions.headers = headers;

    return this.httpClient.get<T>(this.urlApi + path, httpOptions)
      .pipe(catchError(this.formatError));
  }

  public post<T>(path: string, body?: object, params?: HttpParams, headers?: HttpHeaders): Observable<T> {
    const httpOptions = this.getBasicHttpOptions();
    if (params)
      httpOptions.params = params;
    if (headers)
      httpOptions.headers = headers;

    return this.httpClient.post<T>(this.urlApi + path, JSON.stringify(body), httpOptions)
      .pipe(catchError(this.formatError));
  }

  public postOptions<T>(path: string, body: object, newHttpOptions: object): Observable<T> {
    return this.httpClient.post<T>(this.urlApi + path, body, newHttpOptions)
      .pipe(catchError(this.formatError));
  }

  public doRequest(request: HttpRequest<any>) {
    return this.httpClient.request(request)
      .pipe(catchError(this.formatError));
  }

  public delete<T>(path: string, params?: HttpParams) {
    const httpOptions = this.getBasicHttpOptions();
    if (params)
      httpOptions.params = params;

    return this.httpClient.delete<T>(this.urlApi + path, httpOptions)
      .pipe(catchError(this.formatError));
  }

  public put<T>(path: string, body: object, params?: HttpParams) {
    const httpOptions = this.getBasicHttpOptions();
    if (params)
      httpOptions.params = params;

    return this.httpClient.put<T>(this.urlApi + path, body, httpOptions)
      .pipe(catchError(this.formatError));
  }

  private getBasicHttpOptions(): ModelHttpOptions {
    let httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json; charset=utf-8'
      }),
      params: new HttpParams()
    };
    return httpOptions;
  }

  private formatError(error: any) {
    // console.log(error);
    if (error.error) {
      if (error.error.message) {
        return throwError({ message: error.error.message })
      }
    }
    return throwError({ message: "неизвестная ошибка" });
  }

}
