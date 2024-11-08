/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { BorneComponent } from './borne.component';

describe('BorneComponent', () => {
  let component: BorneComponent;
  let fixture: ComponentFixture<BorneComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BorneComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BorneComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
