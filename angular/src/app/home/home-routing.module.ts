import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HomeComponent } from './home.component';
import { PublicHomeComponent } from './public-home.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    { path: 'home', component: PublicHomeComponent },
                    { path: 'adminhome', component: HomeComponent, },
                ],
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class HomeRoutingModule { }
