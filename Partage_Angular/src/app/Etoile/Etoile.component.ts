import { borneService } from './../services/borneService';
import { Component, OnInit , Input, Output, EventEmitter} from '@angular/core';
import { EvaluationDTO } from '../models/EvaluationDTO';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-Etoile',
  templateUrl: './Etoile.component.html',
  styleUrls: ['./Etoile.component.css']
})
export class EtoileComponent {
  @Input() rating: number = 0;
  @Input() readOnly: boolean = false;
  @Output() ratingChange: EventEmitter<number> = new EventEmitter();
  @Output() commentChange: EventEmitter<string> = new EventEmitter();

  stars: boolean[] = Array(5).fill(false);
  comment: string = '';

  constructor() {}

  ngOnInit() {
    this.updateStars();
  
  }

  updateStars() {
    this.stars = this.stars.map((_, i) => i < this.rating);

  }

  setRating(index: number) {
    if (this.readOnly) return;
    this.rating = index + 1;
    this.ratingChange.emit(this.rating);
    this.updateStars();
  }

  onCommentChange(event: Event) {
    if (this.readOnly) return;
    const target = event.target as HTMLTextAreaElement;
    this.comment = target.value;
    this.commentChange.emit(this.comment);
  }


}
