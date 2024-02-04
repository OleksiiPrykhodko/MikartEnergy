import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpResponse, HttpHeaders, HttpParams, HttpErrorResponse } from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';

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
    this._headers = this._headers.set(name, value);
  }

  public deleteHeader(name: string): void {
    this._headers = this._headers.delete(name);
  }

  public getRequest<T>(url: string, httpQueryParams?: HttpParams): Observable<T> {
    return this.http.get<T>(this.buildUrl(url), { headers: this.getHeaders(), params: httpQueryParams })
      .pipe(catchError(this.handleError));
  }

  public getFullRequest<T>(url: string, httpQueryParams?: HttpParams): Observable<HttpResponse<T>> {
    return this.http.get<T>(this.buildUrl(url), { observe: 'response', headers: this.getHeaders(), params: httpQueryParams })
      .pipe(catchError(this.handleError));
  }

  public postClearRequest<T>(url: string, payload: object): Observable<T> {
    return this.http.post<T>(this.buildUrl(url), payload)
      .pipe(catchError(this.handleError));
  }

  public postRequest<T>(url: string, payload: object): Observable<T> {
    return this.http.post<T>(this.buildUrl(url), payload, { headers: this.getHeaders() })
      .pipe(catchError(this.handleError));
  }

  public postFullRequest<T>(url: string, payload: object): Observable<HttpResponse<T>> {
    return this.http.post<T>(this.buildUrl(url), payload, { headers: this.getHeaders(), observe: 'response' })
      .pipe(catchError(this.handleError));
  }

  public putRequest<T>(url: string, payload: object): Observable<T> {
    return this.http.put<T>(this.buildUrl(url), payload, { headers: this.getHeaders() })
      .pipe(catchError(this.handleError));
  }

  public putFullRequest<T>(url: string, payload: object): Observable<HttpResponse<T>> {
    return this.http.put<T>(this.buildUrl(url), payload, { headers: this.getHeaders(), observe: 'response' })
      .pipe(catchError(this.handleError));
  }

  public deleteRequest<T>(url: string, httpQueryParams?: HttpParams): Observable<T> {
    return this.http.delete<T>(this.buildUrl(url), { headers: this.getHeaders(), params: httpQueryParams })
      .pipe(catchError(this.handleError));
  }

  public deleteFullRequest<T>(url: string, httpQueryParams?: HttpParams): Observable<HttpResponse<T>> {
    return this.http.delete<T>(this.buildUrl(url), { headers: this.getHeaders(), observe: 'response', params: httpQueryParams })
      .pipe(catchError(this.handleError));
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

  private handleError(error: HttpErrorResponse) {
    if (error.status === 0) {
      // A client-side or network error occurred.
      console.error("Client-side or network error occurred.");
      //TODO: Add here Call of pop up or toast with info about occurred error.
    } else {
      // The backend returned an unsuccessful response code.
      // The response body contain clues as to what went wrong.
      console.error(`Backend returned code ${error.status}.`);
      //TODO: Add here Call of pop up or toast with info - "Error occurred".
    }
    // Return an observable with a user-facing error message.
    return throwError(error);
  }
}
