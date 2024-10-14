<script setup lang="ts">
import { Avatar, AvatarImage } from '~/components/ui/avatar'
import { Crown, Award, FileBadge2, ShieldCheck, FileText, Settings, PanelLeft, BriefcaseBusiness } from 'lucide-vue-next'
import type { Component } from 'vue'
import { useToggle } from '@vueuse/core'
import { cn } from '~/lib/utils'
import { useUserStore } from '~/src/shared/model/user'

defineSlots<{
  default: (props: unknown) => unknown
}>()

type NavItem = {
  icon: Component
  title: string
  url: string
}
const userStore = useUserStore()
const { user } = storeToRefs(userStore)
// userStore.load()
useAsyncData('load', async () => {
  await userStore.load()
  return true
})

const navItems: NavItem[] = [
  {
    icon: BriefcaseBusiness,
    title: 'Организация',
    url: '/profile/organization',
  },
  {
    icon: Crown,
    title: 'Навыки',
    url: '/profile/skills',
  },
  {
    icon: Award,
    title: 'Достижения',
    url: '/profile/achievements',
  },
  {
    icon: FileBadge2,
    title: 'Дипломы',
    url: '/profile/diploma',
  },
  {
    icon: ShieldCheck,
    title: 'Сертификаты',
    url: '/profile/certificates',
  },
  {
    icon: FileText,
    title: 'Рекомендации',
    url: '/profile/recommendations',
  },
  {
    icon: Settings,
    title: 'Настройки',
    url: '/settings',
  },
]

const [isAsideOpen, toggleIsAsideOpen] = useToggle(true)
const asideWidth = computed(() => isAsideOpen.value ? '320px' : '80px')
const layoutColumns = computed(() => `${asideWidth.value} 1fr`)
</script>

<template>
  <div class="layout text-base text-black overflow-hidden">
    <aside class="[grid-area:aside] bg-slate-50 flex flex-col gap-y-4 pt-6 pb-8 overflow-hidden">
      <button
        class="p-2 w-fit self-end hover:bg-slate-200 transition rounded-lg"
        :class="isAsideOpen ? 'mr-6' : 'mx-0 self-center'"
        type="button"
        @click="toggleIsAsideOpen()"
      >
        <PanelLeft
          class="w-7 h-7 text-slate-500"
          :stroke-width="1.5"
        />
      </button>
      <NuxtLink
        class="flex gap-x-6 hover:bg-slate-200 items-center py-2.5 mx-4 transition-all rounded-lg"
        :class="[isAsideOpen ? 'px-4' : 'w-min px-2.5']"
        to="/"
      >
        <Avatar class="w-7 h-7">
          <AvatarImage
            alt="avatar"
            class="w-7 h-7 object-cover"
            :src="user?.avatar ?? ''"
          />
        </Avatar>
        <span
          :class="cn(['text-ellipsis text-nowrap overflow-hidden opacity-100 transition-all', !isAsideOpen && 'hidden invisible opacity-0'])"
        >{{ user?.firstName }} {{ user?.lastName }}</span>
      </NuxtLink>
      <nav>
        <ul class="flex flex-col gap-y-1 px-4">
          <li
            v-for="nav in navItems"
            :key="nav.url"
            class="w-full"
          >
            <NuxtLink
              class="flex items-center gap-x-6 py-2.5 rounded-lg transition-all hover:bg-slate-200 w-full has-[.link-active]:font-semibold"
              :class="[isAsideOpen ? 'px-4' : 'px-2.5 justify-start w-min']"
              :to="nav.url"
            >
              <component
                :is="nav.icon"
                class="text-slate-500 min-w-7 min-h-7"
                :height="28"
                :stroke-width="1.5"
                :width="28"
              />
              <span
                :class="cn(['transition-all opacity-100', !isAsideOpen && 'hidden invisible opacity-0'])"
              >  {{ nav.title }}</span>
            </NuxtLink>
          </li>
        </ul>
      </nav>
    </aside>
    <main class="[grid-area:main]">
      <slot />
    </main>
  </div>
</template>

<style scoped lang="css">
.layout {
  display: grid;
  grid-template-areas: 'aside main';
  grid-template-columns: v-bind(layoutColumns);
  min-height: 100svh;
  transition: all 0.3s;
}

.router-link-exact-active {
  @apply font-semibold;
};
</style>
