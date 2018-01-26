#include "character.h"
#include "../../nes.h"
#include "../nes/sprite.h"
#include "../nes/controller.h"
#include "../nes/sounds.h"
#include "./utils/math.h"

struct Vector2 playerPosition;

void updateCharacterPos (void) {
  if (Input.player1.Right) {
    drawCharacterRight();
    ++playerPosition.x;
    Sprites[1].flags = Sprites[0].flags &= ~SPRITE_FLAG_FLIP_HORIZONTALLY;
  } else if (Input.player1.Left) {
    drawCharacterLeft();
    --playerPosition.x;
    Sprites[1].flags = Sprites[0].flags |= SPRITE_FLAG_FLIP_HORIZONTALLY;
  }

  if (Input.player1.Up) {
    drawCharacterUp();
    --playerPosition.y;
  } else if (Input.player1.Down) {
    ++playerPosition.y;
    drawCharacterBottom();
  }

  if (Input.player1.A) {
    playSample(0);
  }
 /* Sprites[1].xPos = Sprites[0].xPos = playerPosition.x + 0x80;
  Sprites[0].yPos = playerPosition.y + 0x80;
  Sprites[1].yPos = playerPosition.y + 0x88;*/
}

void updateCharacter(void) {
  updateCharacterPos();
}

void drawCharacterBottom(void) {
  Sprites[0].xPos = 0x80;
  Sprites[0].yPos = 0x80;
  Sprites[0].spriteIndex = 0x01;
  Sprites[0].flags = 0x00;

  Sprites[1].xPos = 0x80;
  Sprites[1].yPos = 0x88;
  Sprites[1].spriteIndex = 0x11;
  Sprites[1].flags = 0x00;

  Sprites[2].xPos = 0x78;
  Sprites[2].yPos = 0x80;
  Sprites[2].spriteIndex = 0x00;
  Sprites[2].flags = 0x00;

  Sprites[3].xPos = 0x78;
  Sprites[3].yPos = 0x88;
  Sprites[3].spriteIndex = 0x10;
  Sprites[3].flags = 0x00;
}

void drawCharacterRight(void) {
  Sprites[0].xPos = 0x80;
  Sprites[0].yPos = 0x80;
  Sprites[0].spriteIndex = 0x03;
  Sprites[0].flags = 0x00;

  Sprites[1].xPos = 0x80;
  Sprites[1].yPos = 0x88;
  Sprites[1].spriteIndex = 0x13;
  Sprites[1].flags = 0x00;

  Sprites[2].xPos = 0x78;
  Sprites[2].yPos = 0x80;
  Sprites[2].spriteIndex = 0x02;
  Sprites[2].flags = 0x00;

  Sprites[3].xPos = 0x78;
  Sprites[3].yPos = 0x88;
  Sprites[3].spriteIndex = 0x12;
  Sprites[3].flags = 0x00;
}

void drawCharacterLeft(void) {
  Sprites[0].xPos = 0x80;
  Sprites[0].yPos = 0x80;
  Sprites[0].spriteIndex = 0x02;
  Sprites[0].flags = SPRITE_FLAG_FLIP_HORIZONTALLY;

  Sprites[1].xPos = 0x80;
  Sprites[1].yPos = 0x88;
  Sprites[1].spriteIndex = 0x12;
  Sprites[1].flags = SPRITE_FLAG_FLIP_HORIZONTALLY;

  Sprites[2].xPos = 0x78;
  Sprites[2].yPos = 0x80;
  Sprites[2].spriteIndex = 0x03;
  Sprites[2].flags = SPRITE_FLAG_FLIP_HORIZONTALLY;

  Sprites[3].xPos = 0x78;
  Sprites[3].yPos = 0x88;
  Sprites[3].spriteIndex = 0x13;
  Sprites[3].flags = SPRITE_FLAG_FLIP_HORIZONTALLY;
}

void drawCharacterUp(void) {
  Sprites[0].xPos = 0x80;
  Sprites[0].yPos = 0x80;
  Sprites[0].spriteIndex = 0x05;
  Sprites[0].flags = 0x00;

  Sprites[1].xPos = 0x80;
  Sprites[1].yPos = 0x88;
  Sprites[1].spriteIndex = 0x15;
  Sprites[1].flags = 0x00;

  Sprites[2].xPos = 0x78;
  Sprites[2].yPos = 0x80;
  Sprites[2].spriteIndex = 0x04;
  Sprites[2].flags = 0x00;

  Sprites[3].xPos = 0x78;
  Sprites[3].yPos = 0x88;
  Sprites[3].spriteIndex = 0x14;
  Sprites[3].flags = 0x00;
}

void startCharacter(void) {
  drawCharacterBottom();
}
