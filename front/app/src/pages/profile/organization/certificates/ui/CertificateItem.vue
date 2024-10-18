<script setup lang="ts">
import { DateFormatter } from '@internationalized/date'
import { VisuallyHidden } from 'radix-vue'

import { Avatar, AvatarFallback, AvatarImage } from '~/src/shared/ui/kit/avatar'
import { Button } from '~/src/shared/ui/kit/button'
import { Dialog, DialogContent, DialogDescription, DialogHeader, DialogTitle, DialogTrigger } from '~/src/shared/ui/kit/dialog'

import type { Certificate } from '../types'

defineProps<{
  certificate: Certificate
}>()

const emit = defineEmits<{
  'certificate-action': [certificate: Certificate]
}>()

const df = new DateFormatter('ru-RU', { dateStyle: 'short' })
</script>

<template>
  <div
    :key="certificate.id"
    class="flex flex-col p-4 bg-slate-50 rounded-lg"
  >
    <div class="">
      <Dialog v-if="certificate.image">
        <DialogTrigger>
          <img

            :alt="certificate.title"
            class="bg-slate-200 w-full object-cover aspect-video rounded-lg"
            :src="certificate.image.url"
          >
        </DialogTrigger>
        <DialogContent>
          <VisuallyHidden>
            <DialogHeader>
              <DialogTitle>Просмотр фотографии</DialogTitle>
              <DialogDescription>Просмотр фотографии</DialogDescription>
            </DialogHeader>
          </VisuallyHidden>
          <div class="max-w-[90vw] max-h-[90svh]">
            <img
              :alt="certificate.title"
              class="w-max object-contain rounded-lg"
              :src="certificate.image.url"
            >
          </div>
        </DialogContent>
      </Dialog>
      <div
        v-else
        class="flex justify-center items-center bg-slate-200 w-full h-full aspect-video rounded-lg"
      />
    </div>
    <span class="font-semibold mt-4">{{ certificate.title }}</span>
    <div class="flex justify-between">
      <span class="font-light">{{ df.format(certificate.issuedDate) }}</span>
    </div>
    <div class="mt-2 flex items-center gap-x-3">
      <Avatar>
        <AvatarImage :src="certificate.issuedUser.avatar ?? ''" />
        <AvatarFallback>{{ certificate.issuedUser.firstName.slice(0, 2) }}</AvatarFallback>
      </Avatar>
      <span class="overflow-hidden text-ellipsis text-nowrap">
        {{ certificate.issuedUser.firstName }} {{ certificate.issuedUser.lastName }}

      </span>
    </div>
    <Button
      class="w-fit mt-2"
      size="xs"
      variant="outline"
      @click="emit('certificate-action', certificate)"
    >
      Подтвердить
    </Button>
  </div>
</template>
