import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CreateOrEditAddPostComponent } from './create-or-edit-add-post/create-or-edit-add-post.component'
import { HttpClientJsonpModule } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { ModalModule } from 'ngx-bootstrap/modal';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { NgxPaginationModule } from 'ngx-pagination';
import { ServiceProxyModule } from '@shared/service-proxies/service-proxy.module';
import { SharedModule } from '@shared/shared.module';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { MainRoutingModule } from './main-routing.module';
import { FileUploadModule } from 'primeng/fileupload';
import { DropdownModule } from 'primeng/dropdown';
import {MultiSelectModule} from 'primeng/multiselect';
import {SliderModule} from 'primeng/slider';

import {TabMenuModule} from 'primeng/tabmenu';;
import { UserDashboardComponent } from './user-dashboard/user-dashboard.component';
import { UserSiderbarComponent } from './user-siderbar/user-siderbar.component'
// import {MenuItem} from 'primeng/api';
@NgModule({
	imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    HttpClientJsonpModule,
    ModalModule.forChild(),
    MainRoutingModule,
    BsDropdownModule,
    CollapseModule,
    TabsModule,
    ServiceProxyModule,
    SharedModule,
    NgxPaginationModule,
    //primeng
    ProgressSpinnerModule,
    DropdownModule,
    TabMenuModule,
    FileUploadModule,
    MultiSelectModule,	
    SliderModule],
	declarations: [
	CreateOrEditAddPostComponent,
	UserDashboardComponent,
	UserSiderbarComponent
],
	providers: []
})
export class MainModule { } 
