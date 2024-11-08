/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { MaBorneComponent } from './ma-borne.component';

describe('MaBorneComponent', () => {
  let component: MaBorneComponent;
  let fixture: ComponentFixture<MaBorneComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MaBorneComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MaBorneComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
