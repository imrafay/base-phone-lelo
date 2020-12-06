import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import Pusher, { Channel } from "pusher-js";
import { Subject, Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class PusherService {
  pusherClient: Pusher;
  channel: Channel;
  constructor() {
    this.pusherClient = new Pusher(environment.pusher.key, {
      cluster: environment.pusher.cluster,
    });

    // this.pusher = new Pusher('xxxxxxxxxxxx', {
    //   authEndpoint: 'http://localhost:3000/pusher/auth',
    // });

    console.log(this.pusherClient);

    let userId = 0;
    if (abp.session.userId) {
      userId = abp.session.userId;
    }
    this.channel = this.pusherClient.subscribe("user-" + userId);
    console.log(this.channel);
  }
}
