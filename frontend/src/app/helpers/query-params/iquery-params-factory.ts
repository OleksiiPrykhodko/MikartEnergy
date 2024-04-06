import { HttpParams } from "@angular/common/http";

export interface IQueryParamsFactory{
    createHttpParams(): HttpParams;
}