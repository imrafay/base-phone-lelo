import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';

@Component({
  selector: 'app-admin-header',
  templateUrl: './admin-header.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AdminHeaderComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
