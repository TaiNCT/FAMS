interface IDictionary<T> {
    [key: string]: T;
}

export default interface BaseResponse<T>{
    statusCode: number;
    message?: string;
    isSuccess: boolean;
    data?: T;
    error?: IDictionary<string[]>;
}