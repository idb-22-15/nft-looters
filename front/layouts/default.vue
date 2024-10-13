<script setup lang="ts">
import { Avatar, AvatarImage } from '~/components/ui/avatar'

defineSlots<{
  default: (props: unknown) => unknown
}>()

type NavItem = {
  title: string
  url: string
}
const navItems: NavItem[] = [
  {
    title: 'Навыки',
    url: '/profile/skills',
  },
  {
    title: 'Достижения',
    url: '/profile/achievements',
  },
  {
    title: 'Дипломы',
    url: '/profile/diploma',
  },
  {
    title: 'Сертификаты',
    url: '/profile/certificates',
  },
  {
    title: 'Настройки',
    url: '/settings',
  },
]
</script>

<template>
  <div class="layout text-base text-black">
    <aside class="aside flex flex-col gap-y-6">
      <div class="flex gap-x-4 items-center px-8">
        <Avatar>
          <AvatarImage
            alt="avatar"
            src="https://github.com/radix-vue.png"
          />
        </Avatar>
        <span class="text-ellipsis text-nowrap overflow-hidden">Матвей Оченьдлиннофамильный</span>
      </div>
      <nav>
        <ul class="flex flex-col gap-y-1 px-2">
          <li
            v-for="nav in navItems"
            :key="nav.url"
            class="w-full"
          >
            <NuxtLink
              class="px-4 pt-1.5 pb-2 rounded-full transition-colors hover:bg-slate-200 w-full block has-[.link-active]:font-semibold"
              :to="nav.url"
            >{{ nav.title }}</NuxtLink>
          </li>
        </ul>
      </nav>
    </aside>
    <main class="main">
      <slot />
    </main>
  </div>
</template>

<style scoped lang="css">
.layout {
  display: grid;
  grid-template-areas: 'aside main';
  grid-template-columns: 320px 1fr;
  min-height: 100svh;
}

.aside {
  @apply bg-slate-100;

  grid-area: aside;
}

.main {
  grid-area: main;
}
</style>
