import { Component, OnInit, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { IProduct } from '../shared/models/product';
import { ShopService } from './shop.service';
import { IBrand } from '../shared/models/brand';
import { IType } from '../shared/models/productType';
import { ShopParams } from '../shared/models/shopParams';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  // @ViewChild('search') searchTerm: ElementRef;
  @ViewChild('search') searchTerm: ElementRef;
  products: Array<IProduct> = [];
  brands: IBrand[];
  types: IType[];

  shopParams = new ShopParams();
  totalCount: number;
  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price: Low to High', value: 'priceAsc' },
    { name: 'Price: High to Low', value: 'priceDesc' }
  ];

  constructor(private shopService: ShopService) {
    // this.shopParams = this.shopService.getShopParams();
  }

  ngOnInit() {
    
    console.log('getItems in onInit');
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts(/*useCache = false*/) {
    this.shopService.getProducts(this.shopParams).subscribe(response => {
      this.products = response.data;
      this.shopParams.pageNumber = response.pageIndex;
      this.shopParams.pageSize = response.pageSize;
      this.totalCount = response.count;
    }, error => {
      console.log(error);
    });
  }

  getBrands() {
    this.shopService.getBrands().subscribe(response => {
      this.brands = [{ id: 0, name: 'All' }, ...response];
    }, error => {
      console.log(error);
    });
  }

  getTypes() {
    this.shopService.getTypes().subscribe(response => {
      this.types = [{ id: 0, name: 'All' }, ...response];
    }, error => {
      console.log(error);
    });
  }

  onBrandSelected(brandId: number) {
    // const params = this.shopService.getShopParams();
    // params.brandId = brandId;
    // params.pageNumber = 1;
    // this.shopService.setShopParams(params);
    this.shopParams.pageNumber = 1;
    this.shopParams.brandId = brandId;
    this.getProducts();
  }

  onTypeSelected(typeId: number) {
    // const params = this.shopService.getShopParams();
    // params.typeId = typeId;
    // params.pageNumber = 1;
    // this.shopService.setShopParams(params);
    this.shopParams.pageNumber = 1;
    this.shopParams.typeId = typeId;
    
    console.log('getItems in onTypeSelected');
    this.getProducts();
  }

  onSortSelected(sort: string) {
    // const params = this.shopService.getShopParams();
    // params.sort = sort;
    // this.shopService.setShopParams(params);
    // this.shopParams.pageNumber = 1;
    this.shopParams.sort = sort;
    this.getProducts();
  }

  onPageChanged(event: any) {
    // const params = this.shopService.getShopParams();
    // if (params.pageNumber !== event) {
    //   params.pageNumber = event;
    //   this.shopService.setShopParams(params);
    //   this.getProducts(true);
    // }
    if(this.shopParams.pageNumber != event){
      this.shopParams.pageNumber = event.page;
      this.getProducts();
    }
    
  }

  onSearch() {
    // const params = this.shopService.getShopParams();
    // params.search = this.searchTerm.nativeElement.value;
    // params.pageNumber = 1;
    // this.shopService.setShopParams(params);
    this.shopParams.pageNumber = 1;
    console.log('I am here');
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.getProducts();
  }

  onReset() {
    // this.searchTerm.nativeElement.value = '';
    // this.shopParams = new ShopParams();
    // this.shopService.setShopParams(this.shopParams);
    this.searchTerm.nativeElement.value = '';
    this.shopParams = new ShopParams();
    this.getProducts();
  }
}
