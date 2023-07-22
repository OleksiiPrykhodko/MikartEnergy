import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HttpInternalService {

  private _baseUrl: string = environment.apiUrl;
  private _headers = new HttpHeaders();

  constructor(private http: HttpClient) { }

  public getHeaders(): HttpHeaders {
    return this._headers;
  }

  public getHeader(name: string): string {
    return this._headers.get(name) ?? "";
  }

  public setHeader(name: string, value: string): void {
    this._headers.set(name, value);
  }

  public deleteHeader(name: string): void {
    this._headers.delete(name);
  }

  public getRequest<T>(url: string, httpQueryParams?: HttpParams): Observable<T> {
    return this.http.get<T>(this.buildUrl(url), { headers: this.getHeaders(), params: httpQueryParams });
  }

  public getFullRequest<T>(url: string, httpQueryParams?: HttpParams): Observable<HttpResponse<T>> {
    return this.http.get<T>(this.buildUrl(url), { observe: 'response', headers: this.getHeaders(), params: httpQueryParams });
  }

  public postClearRequest<T>(url: string, payload: object): Observable<T> {
    return this.http.post<T>(this.buildUrl(url), payload);
  }

  public postRequest<T>(url: string, payload: object): Observable<T> {
    return this.http.post<T>(this.buildUrl(url), payload, { headers: this.getHeaders() });
  }

  public postFullRequest<T>(url: string, payload: object): Observable<HttpResponse<T>> {
    return this.http.post<T>(this.buildUrl(url), payload, { headers: this.getHeaders(), observe: 'response' });
  }

  public putRequest<T>(url: string, payload: object): Observable<T> {
    return this.http.put<T>(this.buildUrl(url), payload, { headers: this.getHeaders() });
  }

  public putFullRequest<T>(url: string, payload: object): Observable<HttpResponse<T>> {
    return this.http.put<T>(this.buildUrl(url), payload, { headers: this.getHeaders(), observe: 'response' });
  }

  public deleteRequest<T>(url: string, httpQueryParams?: HttpParams): Observable<T> {
    return this.http.delete<T>(this.buildUrl(url), { headers: this.getHeaders(), params: httpQueryParams });
  }

  public deleteFullRequest<T>(url: string, httpQueryParams?: HttpParams): Observable<HttpResponse<T>> {
    return this.http.delete<T>(this.buildUrl(url), { headers: this.getHeaders(), observe: 'response', params: httpQueryParams });
  }

  public buildUrl(url: string): string {
    if (url.startsWith('http://') || url.startsWith('https://')) {
      return url;
    }
    return this._baseUrl + url;
  }

  public prepareData(payload: object): string {
    return JSON.stringify(payload);
  }
}
