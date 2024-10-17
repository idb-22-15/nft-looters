<script setup lang="ts">
import { toTypedSchema } from '@vee-validate/zod'
import { Construction } from 'lucide-vue-next'
import { VisuallyHidden } from 'radix-vue'
import { useForm } from 'vee-validate'
import { z } from 'zod'

import { validationErrors } from '~/src/shared/config'
import { useAuthenticatedUser, useUserStore } from '~/src/shared/model/user'
import { Button } from '~/src/shared/ui/kit/button'
import { Dialog, DialogContent, DialogDescription, DialogHeader, DialogTitle } from '~/src/shared/ui/kit/dialog'
import { FormControl, FormField, FormItem, FormLabel, FormMessage } from '~/src/shared/ui/kit/form'
import { Input } from '~/src/shared/ui/kit/input'
import { Popover, PopoverContent, PopoverTrigger } from '~/src/shared/ui/kit/popover'
import { Textarea } from '~/src/shared/ui/kit/textarea'

import { mockRecommendations } from '../__mocks__'
import EmployeeCard from './EmployeeCard.vue'

const user = useAuthenticatedUser()
const userStore = useUserStore()

const createRecommendationSchema = z.object({
  title: z.string({ message: validationErrors.required }).min(5, validationErrors.minChars(5)).max(128, validationErrors.maxChars(128)),
  text: z.string({ message: validationErrors.required }).min(5, validationErrors.minChars(5)).max(1024, validationErrors.maxChars(1024)),
})

const isOpenCreateRecommendationDialog = ref(false)
const receiverId = ref<string | null>(null)
const openCreateRecommendationDialog = (receiverId_: string) => {
  receiverId.value = receiverId_
  isOpenCreateRecommendationDialog.value = true
}

watch(isOpenCreateRecommendationDialog, () => {
  if (!isOpenCreateRecommendationDialog.value) receiverId.value = null
})

const { meta, handleSubmit } = useForm({ validationSchema: toTypedSchema(createRecommendationSchema) })
const createRecommendation = handleSubmit((values) => {
  console.log('create-recommendation', values)
})

const recommendations = ref(mockRecommendations)

const employeesWithUserRecommendation = ref(['1', '3'])
const hasUserRecommendation = (id: string) => employeesWithUserRecommendation.value.includes(id)
</script>

<template>
  <div
    v-if="user.organization"
    class="flex gap-y-16 flex-col mx-8 mt-6 mb-8"
  >
    <Dialog v-model:open="isOpenCreateRecommendationDialog">
      <DialogContent>
        <DialogHeader>
          <DialogTitle>Написать рекомендацию</DialogTitle>
          <VisuallyHidden>
            <DialogDescription>Написать рекомендацию</DialogDescription>
          </VisuallyHidden>
        </DialogHeader>
        <div class="">
          <form
            class="flex flex-col gap-y-4"
            @submit.prevent="createRecommendation"
          >
            <FormField
              v-slot="{ componentField }"
              name="title"
            >
              <FormItem>
                <FormLabel>Название *</FormLabel>
                <FormControl>
                  <Input v-bind="componentField" />
                </FormControl>
                <FormMessage />
              </FormItem>
            </FormField>

            <FormField
              v-slot="{ componentField }"
              name="text"
            >
              <FormItem>
                <FormLabel>Текст *</FormLabel>
                <FormControl>
                  <Textarea v-bind="componentField" />
                </FormControl>
                <FormMessage />
              </FormItem>
            </FormField>
            <Button
              :disabled="!(meta.dirty && meta.valid)"
              type="submit"
            >
              Отправить рекомендательное письмо
            </Button>
          </form>
        </div>
      </DialogContent>
    </Dialog>

    <h1 class="text-4xl font-semibold">
      {{ user.organization.name }}
    </h1>

    <section>
      <h2 class="text-2xl font-semibold">
        Сотрудники
      </h2>
      <div class="mt-4 grid grid-cols-[repeat(auto-fill,minmax(260px,1fr))] gap-x-8 gap-y-5">
        <EmployeeCard
          v-for="employee in user.organization.employees"
          :key="employee.id"
          :employee="employee"
          :has-recommendation-from-me="hasUserRecommendation(employee.id)"
          @create-recommendation="openCreateRecommendationDialog"
        />
      </div>
    </section>

    <section>
      <h3 class="text-2xl font-semibold">
        Рекомендации от коллег
      </h3>
      <div class="mt-3 grid gap-x-8 gap-y-7 grid-cols-[repeat(auto-fill,minmax(300px,1fr))]">
        <EmployeeCard
          v-for="recommendation in recommendations"
          :key="recommendation.id"
          :employee="recommendation.issuer"
          :has-recommendation-from-me="hasUserRecommendation(recommendation.issuer.id)"
          @create-recommendation="openCreateRecommendationDialog"
        >
          <span class="mt-2 block font-semibold text-nowrap text-ellipsis overflow-hidden">{{ recommendation.title }}</span>
          <p class="mt-0.5 text-start text-sm line-clamp-6">
            {{ recommendation.text }}
          </p>
        </EmployeeCard>
      </div>
    </section>

    <section>
      <h3 class="text-2xl font-semibold">
        Рекомендации коллегам
      </h3>
      <div class="mt-3 grid gap-x-8 gap-y-7 grid-cols-[repeat(auto-fill,minmax(300px,1fr))]">
        <EmployeeCard
          v-for="recommendation in recommendations"
          :key="recommendation.id"
          :employee="recommendation.issuer"
          :has-recommendation-from-me="hasUserRecommendation(recommendation.issuer.id)"
          @create-recommendation="openCreateRecommendationDialog"
        >
          <span class="mt-2 block font-semibold text-nowrap text-ellipsis overflow-hidden">{{ recommendation.title }}</span>
          <p class="mt-0.5 text-start text-sm line-clamp-6">
            {{ recommendation.text }}
          </p>
          <Popover>
            <PopoverTrigger :as-child="true">
              <Button
                class="mt-2"
                size="xs"
                variant="secondary"
              >
                Отозвать
              </Button>
            </PopoverTrigger>
            <PopoverContent class="w-full">
              <span class="block">Вы действительно хотите отозвать рекомендацию?</span>
              <Button
                class="mt-2"
                variant="destructiveSecondary"
              >
                Отозвать
              </Button>
            </PopoverContent>
          </Popover>
        </EmployeeCard>
      </div>
    </section>

    <!-- <section>
      <h3 class="text-2xl font-semibold">
        Запрашивают рекомендацию&nbsp;&nbsp;<span class="text-slate-400">{{ recommendationRequesters.length }}</span>
      </h3>
      <div class="mt-3 grid gap-x-8 gap-y-6 grid-cols-[repeat(auto-fill,minmax(300px,1fr))]">
        <div
          v-for="requester in recommendationRequesters"
          :key="requester.id"
          class=""
        >
          <div class="flex gap-x-3 items-center">
            <Avatar>
              <AvatarImage
                :src="requester.avatar ?? ''"
              />
              <AvatarFallback>{{ requester.firstName.slice(0, 2) }}</AvatarFallback>
            </Avatar>
            <span class="text-nowrap text-ellipsis overflow-hidden">{{ requester.firstName }} {{ requester.lastName }}</span>
          </div>
          <Button
            class="mt-3"
            size="xs"
            variant="secondary"
          >
            Отменить запрос
          </Button>
        </div>
      </div>
    </section> -->

    <section class="mt-8">
      <h3 class="text-2xl font-semibold ">
        Опасная зона
      </h3>
      <Popover>
        <PopoverTrigger>
          <Button
            class="mt-2"
            variant="destructiveSecondary"
          >
            Выйти из организации
          </Button>
        </PopoverTrigger>
        <PopoverContent class="w-full">
          <p>Вы действительно хотите выйти?</p>
          <Button
            class="mt-2"
            variant="destructiveSecondary"
            @click="userStore.exitOrganization()"
          >
            Выйти из организации
          </Button>
        </PopoverContent>
      </Popover>
    </section>
  </div>

  <div
    v-else
    class="flex mx-8 mt-6 mb-8 items-center gap-x-3"
  >
    <Construction
      :absolute-stroke-width="true"
      class="w-10 h-10"
      :stroke-width="1.2"
    />
    <span>В данный момент вы не состоите в организации</span>
  </div>
</template>
