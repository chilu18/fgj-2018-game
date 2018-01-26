#include <stdbool.h>
#include "character.h"
#include "../../nes.h"
#include "../nes/sprite.h"
#include "../nes/controller.h"
#include "../nes/sounds.h"
#include "./utils/math.h"

struct Vector2 playerPosition;
int frame = 0;
bool isWalking = false;
unsigned char direction = 0;

void updateCharacterPos (void) {
  isWalking = Input.player1.Right ||
              Input.player1.Left ||
              Input.player1.Up ||
              Input.player1.Down;

  if (Input.player1.Right) {
    direction = 1;
  } else if (Input.player1.Left) {
    direction = 2;
  }

  if (Input.player1.Up) {
    direction = 3;
  } else if (Input.player1.Down) {
    direction = 0;
  }

  switch(direction) {
    case 0:
      drawCharacterBottom();
      break;
    case 1:
      drawCharacterRight();
      break;
    case 2:
      drawCharacterLeft();
      break;
    case 3:
      drawCharacterUp();
      break;
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
  if (isWalking) ++frame;
}

int animationFrame(void) {
  return (frame >> 2) % 4;
}

void drawCharacterBottom(void) {
  if (!isWalking) {
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
    return;
  }


  if (animationFrame() == 0 || animationFrame() == 2) {
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
  } else if (animationFrame() == 1) {
    Sprites[0].xPos = 0x80;
    Sprites[0].yPos = 0x80;
    Sprites[0].spriteIndex = 0x01 + 0x20;
    Sprites[0].flags = 0x00;

    Sprites[1].xPos = 0x80;
    Sprites[1].yPos = 0x88;
    Sprites[1].spriteIndex = 0x11 + 0x20;
    Sprites[1].flags = 0x00;

    Sprites[2].xPos = 0x78;
    Sprites[2].yPos = 0x80;
    Sprites[2].spriteIndex = 0x00 + 0x20;
    Sprites[2].flags = 0x00;

    Sprites[3].xPos = 0x78;
    Sprites[3].yPos = 0x88;
    Sprites[3].spriteIndex = 0x10 + 0x20;
    Sprites[3].flags = 0x00;
  } else if (animationFrame() == 3) {
    Sprites[0].xPos = 0x80;
    Sprites[0].yPos = 0x80;
    Sprites[0].spriteIndex = 0x01 + 0x40;
    Sprites[0].flags = 0x00;

    Sprites[1].xPos = 0x80;
    Sprites[1].yPos = 0x88;
    Sprites[1].spriteIndex = 0x11 + 0x40;
    Sprites[1].flags = 0x00;

    Sprites[2].xPos = 0x78;
    Sprites[2].yPos = 0x80;
    Sprites[2].spriteIndex = 0x00 + 0x40;
    Sprites[2].flags = 0x00;

    Sprites[3].xPos = 0x78;
    Sprites[3].yPos = 0x88;
    Sprites[3].spriteIndex = 0x10 + 0x40;
    Sprites[3].flags = 0x00;
  }
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
