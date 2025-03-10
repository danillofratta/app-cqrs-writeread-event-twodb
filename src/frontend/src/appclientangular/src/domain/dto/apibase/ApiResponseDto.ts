//export interface ApiResponseDto<T> {
//data: {
//  data: T;
//  success: boolean;
//  message: string;
//  errors: ValidationErrorDetailDto[];
//};
//success: boolean;
//message: string;
//  errors: ValidationErrorDetailDto[];
//}

export interface ApiResponseDto<T> {
  success: boolean;
  data?: T; // Optional data
  message?: string;
  errors?: ValidationErrorDetailDto[];
}

export interface ValidationErrorDetailDto {
  field: string;
  message: string;
}
