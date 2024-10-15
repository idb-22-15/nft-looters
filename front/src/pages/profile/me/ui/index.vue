<!-- eslint-disable vue/no-ref-object-reactivity-loss -->
<script setup lang="ts">
import { Button } from '~/src/shared/ui/kit/button'
import { Input } from '~/src/shared/ui/kit/input'
import { Dialog, DialogTrigger, DialogContent, DialogHeader, DialogDescription, DialogTitle } from '~/src/shared/ui/kit/dialog'
import { Calendar } from '~/src/shared/ui/kit/calendar'
import { toDate } from 'radix-vue/date'
import { CalendarDate, DateFormatter, getLocalTimeZone, parseDate, today } from '@internationalized/date'
import { Popover, PopoverTrigger, PopoverContent } from '~/src/shared/ui/kit/popover'
import { Calendar as CalendarIcon } from 'lucide-vue-next'
import { cn } from '~/src/shared/lib/utils'
import { z } from 'zod'
import { useForm } from 'vee-validate'
import { toTypedSchema } from '@vee-validate/zod'
import { FormField, FormControl, FormMessage, FormLabel, FormItem } from '~/src/shared/ui/kit/form'
import { VisuallyHidden } from 'radix-vue'
import { useAuthenticatedUser, useUserStore, type Gender } from '~/src/shared/model/user'
import { Select, SelectItem, SelectContent, SelectValue, SelectTrigger } from '~/src/shared/ui/kit/select'
import MetaMaskIcon from './MetaMaskIcon.vue'

defineOptions({
  name: 'ProfilePage',
})

const mockPhotos = [
  'https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fimages.lifestyleasia.com%2Fwp-content%2Fuploads%2Fsites%2F2%2F2022%2F01%2F14164044%2Fmutant-975x1024-1.jpeg&f=1&nofb=1&ipt=e196d1beae57e458b3ab8da9a2bd7a8d4a9792a4be0fb884a4f094a37ac81549&ipo=images',
  'https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fwallpapers.com%2Fimages%2Ffile%2Fnft-pictures-sxq84lbki4w5hy31.jpg&f=1&nofb=1&ipt=409fd3715613dbb1ecdf55a1794a43dde1d89ff3d0e1a5d0848b00e5ea6082ee&ipo=images',
]

const userStore = useUserStore()
const user = useAuthenticatedUser()

const errorRequired = 'Это обязательное поле'

const schema = z.object({
  avatar: z.string().url().optional(),
  firstName: z.string({ message: errorRequired }).min(1, { message: errorRequired }).max(256),
  lastName: z.string({ message: errorRequired }).max(256),
  birthday: z.string({ message: errorRequired }).date(),
  gender: z.enum(['male', 'female'], { message: errorRequired }),
})

const { values, handleSubmit, setFieldValue, meta } = useForm({ validationSchema: toTypedSchema(schema), initialValues: {
  avatar: user.value.avatar,
  firstName: user.value.firstName,
  lastName: user.value.lastName,
  birthday: user.value.birthday.toString(),
  gender: user.value.gender,
} })

const onSubmit = handleSubmit((values) => {
  userStore.update({ ...user.value,
    firstName: values.firstName,
    lastName: values.lastName,
    gender: values.gender,
    birthday: values.birthday,
  })
})

const df = new DateFormatter('ru-RU', {
  dateStyle: 'long',
})
</script>

<template>
  <div class="mx-8 mt-6 mb-8">
    <h1 class="text-4xl font-semibold hidden">
      Личный кабинет
    </h1>
    <div class="grid items-center mt-4 grid-cols-[max-content_1fr] gap-x-4">
      <div class="">
        <img
          alt="avatar"
          class="object-cover w-32 aspect-square rounded-full"
          :src="values.avatar"
        >
      </div>
      <div class="">
        <Dialog>
          <DialogTrigger :as-child="true">
            <Button variant="outline">
              Выбрать из NFT
            </Button>
          </DialogTrigger>

          <DialogContent
            class="max-h-[100svh] min-w-[90vw] rounded-lg"
          >
            <VisuallyHidden>
              <DialogHeader>
                <DialogTitle>NFT</DialogTitle>
                <DialogDescription>NFT</DialogDescription>
              </DialogHeader>
            </VisuallyHidden>
            <div class="grid grid-cols-5 gap-4">
              <img
                v-for="photo in mockPhotos"
                :key="photo"
                alt=""
                class="cursor-pointer rounded-lg aspect-square object-cover"
                :src="photo"
              >
            </div>
          </DialogContent>
        </Dialog>
        <!-- <Input type="file" /> -->
      </div>
    </div>
    <form
      class="flex mt-6 flex-col max-w-[500px] gap-y-4"
      @submit.prevent="onSubmit"
    >
      <FormField
        v-slot="{ componentField }"
        name="firstName"
      >
        <FormItem>
          <FormLabel>Имя</FormLabel>
          <FormControl>
            <Input v-bind="componentField" />
          </FormControl>
        </FormItem>
        <FormMessage />
      </FormField>

      <FormField
        v-slot="{ componentField }"
        name="lastName"
      >
        <FormItem>
          <FormLabel>Фамилия</FormLabel>
          <FormControl>
            <Input v-bind="componentField" />
          </FormControl>
          <FormMessage />
        </FormItem>
      </FormField>

      <FormField
        v-slot="{ componentField, setTouched }"
        name="birthday"
      >
        <FormItem>
          <FormLabel>День рождения</FormLabel>
          <Popover>
            <PopoverTrigger :as-child="true">
              <FormControl>
                <Button
                  :class="cn(
                    'flex w-full justify-start text-left font-normal',
                    !values.birthday && 'text-muted-foreground',
                  )"
                  variant="outline"
                >
                  <CalendarIcon class="mr-2 h-4 w-4" />
                  {{ values.birthday ? df.format(toDate(parseDate(values.birthday), getLocalTimeZone())) : "Выберете дату" }}
                </Button>
              </FormControl>
            </PopoverTrigger>
            <PopoverContent class="w-auto p-0">
              <Calendar
                :max-value="today(getLocalTimeZone())"
                :min-value="new CalendarDate(1900, 1, 1)"
                :model-value="componentField.modelValue && parseDate(componentField.modelValue)"
                @update:model-value="setFieldValue('birthday', $event?.toString()), setTouched(true)"
              />
            </PopoverContent>
          </Popover>
          <FormMessage />
        </FormItem>
      </FormField>

      <FormField
        v-slot="{ componentField, setTouched }"
        name="gender"
      >
        <FormItem>
          <FormLabel>Пол</FormLabel>
          <Select
            :model-value="componentField.modelValue"
            @update:model-value="setFieldValue('gender', $event as Gender), setTouched(true) "
          >
            <SelectTrigger>
              <FormControl>
                <SelectValue />
              </FormControl>
            </SelectTrigger>
            <SelectContent>
              <SelectItem value="male">
                Мужской
              </SelectItem>
              <SelectItem value="female">
                Женский
              </SelectItem>
            </SelectContent>
          </Select>
          <FormMessage />
        </FormItem>
      </FormField>
      <Button
        :disabled="!(meta.dirty && meta.valid)"
        type="submit"
      >
        Сохранить
      </Button>

      <Button
        class="mt-0 w-full flex items-center"
        type="button"
        variant="secondary"
      >
        Подключить <MetaMaskIcon class="h-10" />
      </Button>
    </form>

    <Button
      class="mt-8"
      type="button"
      variant="destructiveSecondary"
      @click="userStore.logout()"
    >
      Выйти из аккаунта
    </Button>
  </div>
</template>
