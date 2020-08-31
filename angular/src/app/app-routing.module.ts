import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { UsersComponent } from './users/users.component';
import { TenantsComponent } from './tenants/tenants.component';
import { RolesComponent } from 'app/roles/roles.component';
import { ChangePasswordComponent } from './users/change-password/change-password.component';
import { PublicHomeComponent } from './home/public-home.component';
import { CategoryComponent } from './category/category.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                component: AppComponent,
                children: [
                    { path: 'home', component: PublicHomeComponent },
                    { path: 'adminhome', component: HomeComponent, },
                    { path: 'users', component: UsersComponent, data: { permission: 'Pages.Users' }, },
                    { path: 'roles', component: RolesComponent, data: { permission: 'Pages.Roles' }, },
                    { path: 'tenants', component: TenantsComponent, data: { permission: 'Pages.Tenants' }, },
                    { path: 'about', component: AboutComponent },
                    { path: 'update-password', component: ChangePasswordComponent },
                    { path: 'category', component: CategoryComponent },
                    {
                        path: 'main',
                        loadChildren: () => import('app/main/main.module').then(m => m.MainModule), //Lazy load main module
                    },
                ]
            }
        ])
    ],
    exports: [RouterModule]
})
export class AppRoutingModule { }
