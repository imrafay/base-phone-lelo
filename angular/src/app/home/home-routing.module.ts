import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HomeComponent } from './home.component';
import { PublicHomeComponent } from './public-home.component';
import { ProductCategoriesViewComponent } from './product-categories-view/product-categories-view.component';
import { ProductDetailViewComponent } from './product-detail-view/product-detail-view.component';


@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: 'home',
                children: [
                    { path: '', component: PublicHomeComponent },
                    { path: 'adminhome', component: HomeComponent, },
                    { path: 'category/:id', component: ProductCategoriesViewComponent },
                    { path: 'detail/:id', component: ProductDetailViewComponent }
                ],
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class HomeRoutingModule { }
