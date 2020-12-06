import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CreateOrEditAddPostComponent } from './create-or-edit-add-post/create-or-edit-add-post.component';
import { UserDashboardComponent } from './user-dashboard/user-dashboard.component';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { AuthGuard } from '@shared/auth/auth-guard';
import { RatingModule } from 'ngx-bootstrap/rating';
import { MessagesComponent } from './messages/messages.component';


@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                canActivate: [AuthGuard],
                children: [
                    { path: 'add-post', component: CreateOrEditAddPostComponent },
                    { path: 'user-dashboard', component: UserDashboardComponent },
                    { path: 'message/:userId/:userName', component: MessagesComponent },
                    { path: 'message', component: MessagesComponent },
                ],
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class MainRoutingModule { }
