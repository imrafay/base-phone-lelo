import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CreateOrEditAddPostComponent } from './create-or-edit-add-post/create-or-edit-add-post.component';
import { UserDashboardComponent } from './user-dashboard/user-dashboard.component';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';


@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    { path: 'add-post', component: CreateOrEditAddPostComponent, canActivate: [AppRouteGuard] },
                    { path: 'user-dashboard', component: UserDashboardComponent,canActivate: [AppRouteGuard]  },
                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class MainRoutingModule { }
