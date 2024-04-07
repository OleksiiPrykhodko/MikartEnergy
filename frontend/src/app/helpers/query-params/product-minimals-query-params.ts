import { HttpParams } from "@angular/common/http";
import { SortByType } from "../enums/sort-by-type";
import { IQueryParamsFactory } from "./iquery-params-factory";
import { PaginationQueryParams } from "./pagination-query-params";

export class ProductMinimalsQueryParams extends PaginationQueryParams implements IQueryParamsFactory{
    private _productOrderNumber: string = "";
    private _productName: string = "";
    private _sortBy: SortByType = SortByType.OrderNumber;
    private _orderByDescending: boolean = false;

    public get productOrderNumber(): string{
        return this._productOrderNumber;
    }

    public get productName(): string{
        return this._productName;
    }

    public get sortBy(): SortByType{
        return this._sortBy;
    }

    public get orderByDescending(): boolean{
        return this._orderByDescending;
    }

    createHttpParams(): HttpParams {
        var params = new HttpParams()
            .append("pageNumber", this.pageNumber)
            .append("pageSize", this.pageSize)
            .append("productOrderNumber", this.productOrderNumber)
            .append("productName", this.productName)
            .append("sortBy", this.sortBy)
            .append("orderByDescending", this.orderByDescending)

        return params;
    }

    public setProductOrderNumber(orderNumber: string){
        this._productOrderNumber = orderNumber;
    }
    
    public setProductName(name: string){
        this._productName = name;
    }

    public setSortByType(byType: SortByType){
        this._sortBy = byType;
    }

    public setOrderBy(descending: boolean){
        this._orderByDescending = descending;
    }
}