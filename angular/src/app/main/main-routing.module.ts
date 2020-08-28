import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CreateOrEditAddPostComponent } from './create-or-edit-add-post/create-or-edit-add-post.component';
import { UserDashboardComponent } from './user-dashboard/user-dashboard.component';


@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    { path: 'add-post', component: CreateOrEditAddPostComponent  },
                    { path: 'user-dashboard', component: UserDashboardComponent  },
                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class MainRoutingModule { }
