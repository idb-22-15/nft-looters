<script setup lang="ts">
import { useToggle } from '@vueuse/core'
import { LogIn, PanelLeft } from 'lucide-vue-next'

import { cn } from '~/src/shared/lib/utils'
import { isUserProfile, useProfileStore, useUserStore } from '~/src/shared/model/user'
import { Avatar, AvatarFallback, AvatarImage } from '~/src/shared/ui/kit/avatar'
import { buttonVariants } from '~/src/shared/ui/kit/button'

import { organizationNavItems, userNavItems } from '../config'

defineSlots<{
  default: (props: unknown) => unknown
}>()

const profileStore = useProfileStore()
profileStore.load()

const { profile } = storeToRefs(profileStore)

const profileName = computed(() =>
  profile.value === undefined
    ? ''
    : isUserProfile(profile.value)
      ? `${profile.value.firstName} ${profile.value.lastName}`
      : profile.value.name)
// const userStore = useUserStore()
// const { user } = storeToRefs(userStore)
// useAsyncData('load', async () => {
//   await userStore.load()
//   return true
// })
const navItems = computed(() => {
  if (profileStore.isUserProfile) return userNavItems
  else if (profileStore.isOrganizationProfile) return organizationNavItems
  else return []
})

const [isOpenAside, toggleIsAsideOpen] = useToggle(true)
const asideWidth = computed(() => isOpenAside.value ? '320px' : '80px')
const layoutColumns = computed(() => `${asideWidth.value} 1fr`)
</script>

<template>
  <div class="layout text-base text-black overflow-hidden">
    <aside
      class="[grid-area:aside] bg-slate-50 flex flex-col gap-y-4 pt-6 pb-8 overflow-hidden w-full fixed h-full transition-all "
      :style="{ width: asideWidth }"
    >
      <button
        class="p-2 w-fit self-end hover:bg-slate-200 transition rounded-lg"
        :class="isOpenAside ? 'mr-6' : 'mx-0 self-center'"
        type="button"
        @click="toggleIsAsideOpen()"
      >
        <PanelLeft
          class="w-7 h-7 text-slate-500"
          :stroke-width="1.5"
        />
      </button>
      <div
        class=" flex gap-x-4"
        :class="[isOpenAside ? 'mx-8' : 'mx-4']"
      >
        <template v-if="isOpenAside">
          <NuxtLink
            :class="buttonVariants()"
            to="/auth/login"
          >
            Вход
          </NuxtLink>
          <NuxtLink
            :class="buttonVariants({ variant: 'outline' })"
            to="/auth/register"
          >
            Регистрация
          </NuxtLink>
        </template>

        <NuxtLink
          v-else
          :class="cn(buttonVariants({ size: 'icon' }), 'p-2 w-fit h-fit')"
          to="/auth/login"
        >
          <LogIn
            class="text-white w-7 h-7   "
            :stroke-width="1.5"
          />
        </NuxtLink>
      </div>

      <NuxtLink
        v-if="profileStore.isLoggedIn"
        class="flex gap-x-6 hover:bg-slate-200 items-center py-2.5 mx-4 transition-all rounded-lg"
        :class="[isOpenAside ? 'px-4' : 'w-min px-2.5']"
        :to="`/profile/${profileStore.isUserProfile ? 'user' : 'organization'}`"
      >
        <Avatar class="w-7 h-7">
          <AvatarImage
            alt="avatar"
            class="w-7 h-7 object-cover"
            :src="profile?.avatar ?? ''"
          />
          <AvatarFallback>{{ profileName.slice(0, 2) }}</AvatarFallback>
        </Avatar>
        <span
          :class="cn(['text-ellipsis text-nowrap overflow-hidden opacity-100 transition-all',
                      !isOpenAside && 'hidden invisible opacity-0'])"
        >{{ profileName }}</span>
      </NuxtLink>
      <nav>
        <ul class="flex flex-col gap-y-1 px-4">
          <li
            v-for="nav in navItems"
            :key="nav.url"
            class="w-full"
          >
            <NuxtLink
              class="flex items-center gap-x-6 py-2.5 rounded-lg transition-all hover:bg-slate-200 w-full has-[.router-link-active]:font-semibold"
              :class="[isOpenAside ? 'px-4' : 'px-2.5 justify-start w-min']"
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
                :class="cn(['transition-all opacity-100', !isOpenAside && 'hidden invisible opacity-0'])"
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
  grid-template-columns: auto 1fr;
  grid-template-columns: v-bind(layoutColumns);
  min-height: 100svh;
  transition: all 0.3s;
}

.router-link-exact-active {
  @apply font-semibold;
};
</style>
