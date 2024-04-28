export interface PaginationResponse<T>{
    totalItemsNumber: number;
    items: T[];
}