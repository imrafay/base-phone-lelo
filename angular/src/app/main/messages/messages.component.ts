import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { ProductAdvertServiceProxy, ProductAdvertViewDto, ProductAdvertDetailViewDto, SiteStatisticsOutputDto, UserProfileReviewInputDto, ChatMessageInputDto, ChatServiceProxy, ChatMessageDto } from '@shared/service-proxies/service-proxies';
import { CreateOrEditAddPostComponent } from '../create-or-edit-add-post/create-or-edit-add-post.component';
import { AbpSessionService } from 'abp-ng2-module';
import { Router, NavigationExtras, ActivatedRoute } from '@angular/router';
import { AppComponentBase } from '@shared/app-component-base';
import { PusherService } from '@shared/pusher-service.service';
import { environment } from 'environments/environment';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.less']
})

export class MessagesComponent extends AppComponentBase implements OnInit {

  messages: ChatMessageDto[] = [];
  receiverId;
  receiverName = '';
  userMessagesCount: number;
  messageText = '';
  ChatMessageInputDto: ChatMessageInputDto = new ChatMessageInputDto();
  constructor(
    private _ProductAdvertService: ProductAdvertServiceProxy,
    injector: Injector,
    private router: Router,
    private _sessionService: AbpSessionService,
    private pusherService: PusherService,
    private _chatService: ChatServiceProxy,
    private _route: ActivatedRoute,

  ) {
    super(injector);
    this.receiverId = this._route.params ? Number(this._route.params['value'].userId) : ''
    this.receiverName = this._route.params ? this._route.params['value'].userName : ''
    console.log(this._route.params);
    console.log(this.receiverName);
  }


  ngOnInit(): void {
    this.messages = [];
    this.recieveEvent();
    this.receiverId && this.appSession.userId ? this.getAllMessages() : null;
  }

  recieveEvent() {
    this.pusherService.channel.bind('chat-message', (message) => {
      console.log(message);
      this.getAllMessages();
    });
  }


  sendMessage() {
    this.ChatMessageInputDto.senderId = this.appSession.userId;
    this.ChatMessageInputDto.receiverId = this.receiverId;
    // this._chatService.create(this.ChatMessageInputDto).subscribe(res => {
    //   console.log(res)
    //   this.pusherService.channel.trigger('test-user', (res) => {
    //     console.log(res)
    //   })
    //   this.ChatMessageInputDto.message = '';
    // })
    this._chatService.testMessage(this.ChatMessageInputDto.receiverId,this.ChatMessageInputDto.message).subscribe(res => {
    })
  
  }

  getAllMessages() {
    this._chatService.getAll(this.receiverId, this.appSession.userId, undefined, undefined, undefined).subscribe(res => {
      console.log(res);
      this.userMessagesCount = res.items.length;
      this.messages = res.items;
    })
  }

  generateAvater(nameString: string, idTag: string) {
    var colours = ["#1abc9c", "#2ecc71", "#3498db", "#9b59b6", "#34495e", "#16a085", "#27ae60", "#2980b9", "#8e44ad", "#2c3e50", "#f1c40f", "#e67e22", "#e74c3c", "#ecf0cc", "#95a5a6", "#f39c12", "#d35400", "#c0392b", "#bdc3c7", "#7f8c8d"];
    var name = nameString,
      nameSplit = name ? name.split(" ") : '',
      initials = nameSplit ? nameSplit[0].charAt(0).toUpperCase() : '';
    var recipientName = document.getElementById(idTag)
    recipientName.innerText = initials;
    var charIndex = initials.charCodeAt(0) - 64,
      colourIndex = charIndex % 20;
    colourIndex < 1 ? colourIndex = 1 : null;
    recipientName.style.background = colours[colourIndex - 1];
    recipientName.style.textAlign = "center";
    recipientName.style.color = "white";
  }
}
interface Message {
  text: string;
  user: string;
}