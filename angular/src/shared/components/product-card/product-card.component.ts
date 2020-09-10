import { Component, OnInit,Input } from '@angular/core';
import { ProductAdvertServiceProxy, ProductAdvertViewDto } from '@shared/service-proxies/service-proxies';

@Component({
  selector: 'product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.css']
})
export class ProductCardComponent implements OnInit {

  constructor() { }

  @Input('products') products: ProductAdvertViewDto[] = [];
  itemsPerSlide = 4;
  singleSlideOffset = true;
  ngOnInit(): void {
  }

}
