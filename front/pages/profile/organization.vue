<script setup lang="ts">
import { toTypedSchema } from '@vee-validate/zod'
import { VisuallyHidden } from 'radix-vue'
import { useForm } from 'vee-validate'
import { z } from 'zod'
import { buttonVariants } from '~/components/ui/button'
import { useToast } from '~/components/ui/toast'
import { cn } from '~/lib/utils'
import { validationErrors } from '~/src/shared/config'
import { useAuthenticatedUser } from '~/src/shared/model/user'

const { toast } = useToast()
const user = useAuthenticatedUser()

const createRecommendationSchema = z.object({
  title: z.string({ message: validationErrors.required }).min(5, validationErrors.minChars(5)).max(128, validationErrors.maxChars(128)),
  text: z.string({ message: validationErrors.required }).min(5, validationErrors.minChars(5)).max(1024, validationErrors.maxChars(1024)),
})

const { values, meta, handleSubmit } = useForm({ validationSchema: toTypedSchema(createRecommendationSchema) })
const createRecommendation = handleSubmit((values) => {
  console.log('create-recommendation', values)
})

const sendRecommendationRequest = () => {
  toast({ title: 'Запрос отправлен' })
}

const isMe = (id: string) => user.value.id === id
</script>

<template>
  <div
    v-if="user.organization"
    class="flex flex-col mx-8 mt-6 mb-8"
  >
    <h1 class="text-4xl font-semibold">
      {{ user.organization.name }}
    </h1>

    <h2 class="mt-6 text-2xl font-semibold">
      Сотрудники
    </h2>
    <div class="mt-4 grid grid-cols-[repeat(auto-fill,minmax(260px,1fr))] gap-6">
      <div
        v-for="employee in user.organization.employees"
        :key="employee.id"
        class=""
      >
        <div class="flex items-center gap-x-3">
          <Avatar>
            <AvatarImage :src="employee.avatar || ''" />
            <AvatarFallback>{{ employee.firstName.slice(0, 2) }}</AvatarFallback>
          </Avatar>
          <span
            v-if="isMe(employee.id)"
            class="font-semibold"
          >Это я</span>
          <Popover v-else>
            <PopoverTrigger class="text-nowrap text-ellipsis overflow-hidden">
              {{ employee.lastName }} {{ employee.firstName }}
            </PopoverTrigger>
            <PopoverContent class="flex flex-col w-full">
              <NuxtLink
                :class="cn(buttonVariants({ variant: 'ghost' }), 'w-full justify-start')"
                :to="`/users/${employee.id}`"
              >
                Перейти в профиль
              </NuxtLink>
              <Dialog>
                <DialogTrigger>
                  <Button
                    class="w-full justify-start"
                    variant="ghost"
                  >
                    Написать рекомендацию
                  </Button>
                </DialogTrigger>
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
                          <FormLabel>Название</FormLabel>
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
                          <FormLabel>Текст</FormLabel>
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
              <Button
                class="w-full justify-start"
                variant="ghost"
                @click="sendRecommendationRequest"
              >
                Запросить рекомендацию
              </Button>
            </PopoverContent>
          </Popover>
        </div>
      </div>
    </div>
    <section>
      <h3 class="text-2xl font-semibold">
        Опасная зона
      </h3>
    </section>
  </div>

  <div
    v-else
    class="flex flex-col mx-8 mt-6 mb-8"
  >
    В данный момент вы не состоите в организации
  </div>
</template>
