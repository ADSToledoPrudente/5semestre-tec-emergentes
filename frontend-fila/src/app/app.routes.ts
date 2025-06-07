import { Routes } from '@angular/router';
import { LoginComponent } from './view/login/component/component.component';
import { QueueViewComponent } from './view/home/queue-view/queue-view.component';
import { QueuesComponent } from './view/home/queues/queues.component';

export const routes: Routes = [
  { path: '', component: QueuesComponent },
  { path: 'login', component: LoginComponent },
  { path: 'queues', component: QueuesComponent },
  { path: 'queue/:id', component: QueueViewComponent}
];
