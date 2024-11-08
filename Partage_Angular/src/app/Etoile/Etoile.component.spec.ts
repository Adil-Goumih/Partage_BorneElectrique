/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { EtoileComponent } from './Etoile.component';

describe('EtoileComponent', () => {
  let component: EtoileComponent;
  let fixture: ComponentFixture<EtoileComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EtoileComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EtoileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
