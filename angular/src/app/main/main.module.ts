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
    FileUploadModule,
	],
	declarations: [
	CreateOrEditAddPostComponent,
],
	providers: []
})
export class MainModule { } 
