import { KeyValue } from "@angular/common";

export interface  RequestResult<T>{
    dto: T;
    successful: boolean;
    errors: KeyValue<string, string>[];
}