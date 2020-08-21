import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CreateOrEditAddPostComponent } from './create-or-edit-add-post/create-or-edit-add-post.component';


@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    { path: 'add-post', component: CreateOrEditAddPostComponent  }
                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class MainRoutingModule { }
