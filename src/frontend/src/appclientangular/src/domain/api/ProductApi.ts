import { Injectable } from '@angular/core';
import { API } from './API';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { ProductDto } from '../dto/ProductDto';
import { environment } from '../../environments/environment.development';
import { Observable } from 'rxjs';
import { ApiResponseDto } from '../dto/apibase/ApiResponseDto';
import { ProductCreateCommand } from '../dto/Product/Command/ProductCreateCommand';
import { ProductUpdateCommand } from '../dto/Product/Command/ProductUpdateCommand';


@Injectable({
    providedIn: 'root'
})
export class ProductApi extends API {
   
  constructor(
    protected override http: HttpClient,    
    protected override router: Router
  ) {
    super(http, router);

    this._baseurlcommnad = environment.ApiUrlProductCommnand;
    this._baseurlquery = environment.ApiUrlProductQuery;
    this._endpointcommand = "api/v1/productcommand"
    this._endpointquery = "api/v1/productquery"

  }
  
  async GetListAll(): Promise<Observable<ApiResponseDto<ProductDto[]>>> {
    return this._http.get<ApiResponseDto<ProductDto[]>>(`${this._baseurlquery + this._endpointquery}`);
  }

  async GetById(id: string): Promise<Observable<ApiResponseDto<ProductDto>>> {
    return this._http.get<ApiResponseDto<ProductDto>>(`${this._baseurlquery + this._endpointquery + '/getbyid/' + id}`);
  }

  async GetByName(name: string): Promise<Observable<ProductDto[]>>{
    return this._http.get<ProductDto[]>(`${this._baseurlquery + this._endpointquery + '/getbyname/' + name}`);
  }

  async Save(dto: ProductDto): Promise<Observable<ApiResponseDto<ProductCreateCommand>>> {
    return this._http.post<ApiResponseDto<ProductDto>>(`${this._baseurlcommnad + this._endpointcommand}`, dto);
   }

  async Update(dto: ProductDto): Promise<Observable<ApiResponseDto<ProductUpdateCommand>>> {
    return this._http.put<ApiResponseDto<ProductDto>>(`${this._baseurlcommnad + this._endpointcommand}`, dto);
    }

  async Delete(id: number): Promise<Observable<void>>{
    return this._http.delete<void>(`${this._baseurlcommnad + this._endpointcommand +'/'+ id}`);
  }

}
