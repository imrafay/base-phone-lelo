import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientJsonpModule } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { ModalModule } from 'ngx-bootstrap/modal';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { NgxPaginationModule } from 'ngx-pagination';
import { ServiceProxyModule } from '../../shared/service-proxies/service-proxy.module';
import { SharedModule } from '../../shared/shared.module';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { HomeRoutingModule } from './home-routing.module';
import { FileUploadModule } from 'primeng/fileupload';
import { DropdownModule } from 'primeng/dropdown';
import { MultiSelectModule } from 'primeng/multiselect';
import { SliderModule } from 'primeng/slider';
import { TabMenuModule } from 'primeng/tabmenu'; import { HomeComponent } from './home.component';
import { PublicHomeComponent } from './public-home.component';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { ProductCategoriesViewComponent } from './product-categories-view/product-categories-view.component';
import { ProductDetailViewComponent } from './product-detail-view/product-detail-view.component';
import { ProfileAdDashboard } from './profile-ad-dashboard/profile-ad-dashboard';
import { NgxImageZoomModule } from 'ngx-image-zoom';
// import { RatingModule } from 'ngx-bootstrap/rating';
import { ProgressbarModule } from 'ngx-bootstrap/progressbar';
import {RatingModule} from 'primeng/rating';

// import {MenuItem} from 'primeng/api';
@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        HttpClientJsonpModule,
        ModalModule.forChild(),
        RatingModule,
        HomeRoutingModule,
        BsDropdownModule,
        CollapseModule,
        TabsModule,
        ServiceProxyModule,
        SharedModule,
        NgxPaginationModule,
        CarouselModule.forRoot(),
        ProgressbarModule.forRoot(),
        //primeng
        ProgressSpinnerModule,
        DropdownModule,
        TabMenuModule,
        FileUploadModule,
        MultiSelectModule,
        NgxImageZoomModule,
        SliderModule],
    declarations: [
        HomeComponent,
        PublicHomeComponent,
        ProductCategoriesViewComponent,
        ProductDetailViewComponent,
        ProfileAdDashboard,
        ],
    providers: [],
    
})
export class HomeModule { } 
