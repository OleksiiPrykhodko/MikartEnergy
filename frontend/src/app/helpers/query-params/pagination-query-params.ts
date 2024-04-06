import { IQueryParamsFactory } from "./iquery-params-factory";

export abstract class PaginationQueryParams{
    private _minPageNumber = 1;
    private _minPageSize = 1;
    private _maxPageSize = 50;
    
    private _pageNumber: number = this._minPageNumber;
    private _pageSize: number = this._maxPageSize;

    public get minPageSize(): number{
        return this._minPageSize
    }

    public get maxPageSize(): number{
        return this._maxPageSize
    }

    public get pageNumber(): number{
        return this._pageNumber;
    }

    public setPageNumber(number: number): boolean{
        if(number >= this._minPageNumber){
            this._pageNumber = number;
            return true;
        }
        return false;
    }

    public get pageSize(): number{
        return this._pageSize;
    }

    public setPageSize(size: number): boolean{
        if(size >= this._minPageSize && size <= this._maxPageSize){
            this._pageSize = size;
            return true;
        }
        return false;
    }
}