import { AfterViewInit, ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { ProductDto } from '../../../../domain/dto/ProductDto';
import { catchError, delay, finalize, firstValueFrom, Observable, of } from 'rxjs';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { ProductApi } from '../../../../domain/api/ProductApi';
import { ProductSignalRSevice } from '../../../../domain/api/ProductSignalRSevice';
import { ApiResponseDto } from '../../../../domain/dto/apibase/ApiResponseDto';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css'],
})
export class ProductComponent implements OnInit, AfterViewInit {
  public form: FormGroup;
  public busy = false;
  public _ListError: string[] = [];

  dataSource = new MatTableDataSource<ProductDto>(); // Fixed type
  displayedColumns = ['actions', 'id', 'name', 'price'];

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private api: ProductApi,
    private fb: FormBuilder,
    private cdr: ChangeDetectorRef,
    private signal: ProductSignalRSevice
  ) {
    this.form = this.fb.group({
      id: [null],
      name: ['', Validators.required],
      price: [null, [Validators.required, Validators.min(0)]],
    });
  }

  ngOnInit(): void {
    this.signal.onGetListUpdated((updatedDataList: ProductDto[]) => {
      this.dataSource.data = updatedDataList;
      console.log('SignalR updated data:', updatedDataList);
      this.cdr.detectChanges();
    });
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.loadList();
  }

  new() {
    this.form.reset();
    this.form.patchValue({ id: null, name: '', price: null });
    this.cdr.detectChanges();
  }

  async save() {
    if (this.form.valid) {
      this.busy = true;
      const product = this.form.value as ProductDto;
      const observable = product.id ? await this.api.Update(product) : await this.api.Save(product);

      observable.subscribe({
        next: (response) => {
          if (response.success) {
            this.form.reset();
            this.loadList();
          } else {
            this._ListError.push(response.message || 'Failed to save');
          }
        },
        error: (error) => {
          console.error('Error:', error);
          this._ListError.push(error.message || 'Erro ao salvar');
        },
        complete: () => {
          this.busy = false;
          this.cdr.detectChanges();
        }
      });
    }
  }

  async loadList() {
    this.busy = true;
    try {
      const observable = await this.api.GetListAll();
      observable.subscribe({
        next: (response: ApiResponseDto<ProductDto[]>) => {
          if (response.success && response.data) {
            this.dataSource.data = response.data; // Assign only if data exists
            console.log('Data loaded:', this.dataSource.data);
          } else {
            this.dataSource.data = []; // Default to empty array if no data
            this._ListError.push(response.message || 'No data returned from API');
            console.warn('API response unsuccessful or empty:', response);
          }
          this.cdr.detectChanges(); // Trigger UI update
        },
        error: (error) => {
          console.error('API Error:', error);
          this.dataSource.data = []; // Set to empty array on error
          this._ListError.push(error.message || 'Erro ao carregar dados');
          this.cdr.detectChanges();
        },
        complete: () => {
          this.busy = false;
          this.cdr.detectChanges();
        }
      });
    } catch (error) {
      console.error('Unexpected error:', error);
      this.dataSource.data = []; // Set to empty array on unexpected error
      this._ListError.push('Erro inesperado ao carregar lista');
      this.busy = false;
      this.cdr.detectChanges();
    }
  }

  async onUpdate(id: string) {
    this.busy = true;
    try {
      const observable = await this.api.GetById(id); // Get the Observable
      const response: ApiResponseDto<ProductDto> = await firstValueFrom(observable); // Resolve the Observable
      if (response.success && response.data) {
        this.form.patchValue(response.data); // Patch with the single ProductDto
        this.cdr.detectChanges();
      } else {
        this._ListError.push(response.message || 'Nenhum produto encontrado');
        console.warn('API response unsuccessful or no data:', response);
      }
    } catch (error) {
      console.error('Erro ao carregar produto:', error);
      this._ListError.push('Erro ao carregar produto para atualização');
    } finally {
      this.busy = false;
      this.cdr.detectChanges();
    }
  }

  async onDelete(id: number) {
    this.busy = true;
    try {
      await firstValueFrom(await this.api.Delete(id));
      await this.loadList();
    } catch (error) {
      console.error('Erro ao deletar produto:', error);
      this._ListError.push('Erro ao deletar produto');
    } finally {
      this.busy = false;
      this.cdr.detectChanges();
    }
  }
}

//import { AfterViewInit, ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
//import { ProductDto } from '../../../../domain/dto/ProductDto';
//import { catchError, delay, finalize, firstValueFrom, Observable, of } from 'rxjs';
//import { FormBuilder, FormGroup, Validators } from '@angular/forms';
//import { MatTableDataSource } from '@angular/material/table';
//import { MatPaginator } from '@angular/material/paginator';
//import { ProductApi } from '../../../../domain/api/ProductApi';
//import { ProductSignalRSevice } from '../../../../domain/api/ProductSignalRSevice';
//import { ApiResponseDto } from '../../../../domain/dto/apibase/ApiResponseDto';

//@Component({
//  selector: 'app-product',
//  templateUrl: './product.component.html',
//  styleUrls: ['./product.component.css'],
//})
//export class ProductComponent implements OnInit, AfterViewInit {
//  public list$: any[] = [];
//  public form: FormGroup;
//  public busy = false;

//  public _ListError: string[] = [];

//  dataSource = new MatTableDataSource<ProductDto>();
//  displayedColumns = ['actions', 'id', 'name', 'price'];

//  @ViewChild(MatPaginator) paginator!: MatPaginator;

//  constructor(
//    private api: ProductApi,
//    private fb: FormBuilder,
//    private cdr: ChangeDetectorRef,
//    private signal: ProductSignalRSevice
//  ) {
//    //this.initForm();
//        this.form = this.fb.group({
//          id: [''],
//          name: ['', Validators.required],
//          price: ['', [Validators.required, Validators.min(0)]],
//        });
//  }

//  private initForm() {
//    this.form = this.fb.group({
//      id: [null],
//      name: ['', Validators.required],
//      price: [null, [Validators.required, Validators.min(0)]],
//    });
//  }

//  ngAfterViewInit(): void {
//    this.dataSource.paginator = this.paginator;
//    this.loadList();
//  }

//  async ngOnInit() {
//    await this.loadList();
//    this.signal.onGetListUpdated((updatedDataList) => {
//      const product$: ProductDto[] = updatedDataList as ProductDto[];
//      this.dataSource.data = product$;
//      this.cdr.detectChanges();
//    });
//  }

//  new() {  
//    this.form.reset();
//    this.form.patchValue({
//      id: null,
//      name: '',
//      price: null
//    });
//    this.cdr.detectChanges();
//  }

//  async save() {
//    if (this.form.valid) {
//      this.busy = true;
//      const product = this.form.value as ProductDto;

//      const observable = product.id ? await this.api.Update(product) : await this.api.Save(product);

//      observable.subscribe({
//        next: (response) => {
//          if (response.success) {
//            this.initForm(); 
//            this.cdr.detectChanges();
//            this.loadList();
//          } else {
//            this._ListError.push(response.message);
//          }
//        },
//        error: (error) => {
//          console.error('Error:', error);
//          this._ListError.push(error.message);
//        },
//        complete: () => {
//          this.busy = false;
//        }
//      });
//    }
//  }

//  async loadList() {
//    this.busy = true;
//    try {
//      const observable = await this.api.GetListAll();
//      observable.subscribe({
//        next: (response: ApiResponseDto<ProductDto[]>) => {
//          if (response.success && response.data) {
//            this.dataSource.data = response.data;
//            console.log('Data loaded:', this.dataSource.data); // Debug
//            this.cdr.detectChanges();
//          } else {
//            this._ListError.push(response.message || 'No data returned from API');
//            console.warn('API response unsuccessful:', response);
//          }
//        },
//        error: (error) => {
//          console.error('Error:', error);
//          this._ListError.push(error.message || 'Erro ao carregar dados');
//        },
//        complete: () => {
//          this.busy = false;
//        }
//      });
//    } catch (error) {
//      console.error('Unexpected error:', error);
//      this.busy = false;
//    }
//  }

//  async onUpdate(id: number) {
//    this.busy = true;

//    try {
//      const product = await firstValueFrom(await this.api.GetById(id));
//      if (product) {
//        this.form.patchValue(product);
//      }
//    } catch (error) {
//      console.error('Erro ao carregar produto para atualização:', error);
//    } finally {
//      this.busy = false;
//    }
//  }

//  async onDelete(id: number) {
//    this.busy = true;

//    try {
//      await firstValueFrom(await this.api.Delete(id));
//      await this.loadList();
//    } catch (error) {
//      console.error('Erro ao deletar o produto:', error);
//    } finally {
//      this.busy = false;
//    }
//  }
//}


