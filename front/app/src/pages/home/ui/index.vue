<script setup lang="ts">
import { buttonVariants } from '~/src/shared/ui/kit/button'
</script>

<template>
  <div class="main">
    <div class="w-full text-4xl flex flex-col justify-center items-center">
      <div class="slide ">
        <span class="text-8xl looters">NFT Looters</span>
      </div>
      <div class="slide ">
        <span class="">Революция в подтверждении навыков</span>
      </div>
      <div class="slide ">
        <span class="">С использованием NFT и Verifiable Credentials</span>
      </div>
      <div class="slide">
        <span class="error looters">Но</span>
      </div>
      <div class="slide ">
        <span class="">Без NFT</span>
      </div>
      <div class="slide ">
        <span class="">Без Verifiable Credentials</span>
      </div>
      <div class="slide ">
        <span class="">Без Смс и регистрации</span>
      </div>
      <div class="slide ">
        <span class="">Потому что</span>
      </div>
      <div class="slide ">
        <span class="">Мы не справились </span>
      </div>
      <div class="slide ">
        <NuxtLink
          :class="buttonVariants({ size: 'lg' })"
          to="/profile/user"
        >
          Смотреть, что получилось
        </NuxtLink>
      </div>
    </div>
  </div>
</template>

<style scoped>
@keyframes fade-in {
    from {
      scale: 0.5;
      opacity: 0;
    }
    to {
      scale: 1;
      opacity: 1;
    }
  }

  @keyframes error {
    from {
      color: black;
    }
    to {
      /* background: #000; */
      /* color: red; */
      /* font-weight:900; */
    }
  }

  @keyframes fade-out {
    from {
      scale: 1;
      opacity: 1;
    }
    to {
      scale: 0.9;
      opacity: 0;

    }
  }
  .main {
    height: 500dvh;
  }

  .slide {
    @apply h-[100dvh] flex items-center justify-center;

    view-timeline-name: --slide;
    /* view-timeline-inset: 30%; */
  }

  span {
    animation: fade-in linear both, fade-out linear both;
    animation-timeline: --slide;

    animation-range: cover 20% cover 45%, cover 65% exit 50%;
  }

  @property --morf {
    inherits: false;
    initial-value: 25;
    syntax: '<number>';
  }

  @keyframes looters {
  from {
    --morf: 0;
  }

  to {
    --morf: 25;
  }
 }
/*
  @font-face {
    font-family: 'Honk';
    src: url("/Honk.ttf") format('ttf');
    font-weight: normal;
    font-style: normal;
    font-display: swap;
  } */

  /* @font-pallete-values --honk {
    font-family: 'Honk';
    base-pallete: 3;
  } */

  .looters {

    font-family: 'Honk';
    font-feature-settings:"salt";
    /* font-palette: --honk; */
    /* base-palette: 1; */
    font-variation-settings: "MORF" 45, "SHLN" 0;
    /* animation: looters 1s linear infinite alternate; */
  }

  .error {
    animation: fade-in linear both, error linear both;
    animation-timeline: --slide;

    animation-range: cover 20% cover 45%;
  }
</style>
