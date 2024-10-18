<script setup lang="ts">
import { cn } from '~/src/shared/lib/utils'
import { Avatar, AvatarFallback, AvatarImage } from '~/src/shared/ui/kit/avatar'
import { Button, buttonVariants } from '~/src/shared/ui/kit/button'
import { Popover, PopoverContent, PopoverTrigger } from '~/src/shared/ui/kit/popover'

defineSlots<{
  default: () => unknown
}>()

const props = defineProps<{
  employee: {
    id: string
    firstName: string
    lastName: string
    avatar?: string
  }
  hasRecommendationFromMe: boolean
}>()

// const emit = defineEmits<{
//   'create-recommendation': [id: string]
// }>()
</script>

<template>
  <div>
    <div class="flex items-center gap-x-3">
      <Avatar class="relative overflow-visible">
        <AvatarImage
          class="rounded-full"
          :src="props.employee.avatar || ''"
        />
        <AvatarFallback class="rounded-full">
          {{ props.employee.firstName.slice(0, 2) }}
        </AvatarFallback>
      </Avatar>

      <Popover>
        <PopoverTrigger class="text-nowrap text-ellipsis overflow-hidden">
          {{ props.employee.lastName }} {{ props.employee.firstName }}
        </PopoverTrigger>
        <PopoverContent class="flex flex-col w-full">
          <NuxtLink
            :class="cn(buttonVariants({ variant: 'ghost' }), 'w-full justify-start')"
            :to="`/users/${props.employee.id}`"
          >
            Перейти в профиль
          </NuxtLink>
          <!-- <Button
            v-if="!props.hasRecommendationFromMe"
            class="w-full justify-start"
            variant="ghost"
            @click="emit('create-recommendation', employee.id)"
          >
            Написать рекомендацию
          </Button> -->
        </PopoverContent>
      </Popover>
    </div>
    <slot />
  </div>
</template>
