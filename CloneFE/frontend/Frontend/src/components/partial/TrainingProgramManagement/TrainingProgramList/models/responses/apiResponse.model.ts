interface IDictionary<T> {
  [key: string]: T;
}

export interface ApiResponse<T> {
  statusCode: number;
  message: string;
  errors: IDictionary<string[]>;
  data: T;
}
