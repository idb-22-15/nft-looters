<script setup lang="ts">
import { DateFormatter } from '@internationalized/date'
import { Check, ShieldCheck, Shield } from 'lucide-vue-next'
import { Tooltip, TooltipProvider, TooltipContent, TooltipTrigger } from '~/components/ui/tooltip'
import { Dialog, DialogContent, DialogHeader, DialogTitle, DialogDescription, DialogTrigger } from '~/components/ui/dialog'
import { FormField, FormItem, FormLabel, FormControl, FormMessage } from '~/components/ui/form'
import { Button } from '~/components/ui/button'
import { VisuallyHidden } from 'radix-vue'
import { z } from 'zod'
import { useForm } from 'vee-validate'
import { toTypedSchema } from '@vee-validate/zod'
import { useToast } from '~/components/ui/toast'

type Certificate = {
  id: string
  title: string
  issuedDate: Date
  issuerId: string
  verified: boolean
  image?: {
    url: string
  }
}

const mockCertificates: Certificate[] = [
  {
    id: '1',
    title: 'Сертификат безопасности',
    issuedDate: new Date('2023-03-08'),
    issuerId: 'ISACA',
    verified: true,
    image: {
      url: 'https://calendarbox.ru/wp-content/uploads/2019/05/shablon-sertificata-02.jpg',
    },
  },
  {
    id: '2',
    title: 'Сертификат соответствия',
    issuedDate: new Date('2022-12-15'),
    issuerId: 'ISO',
    verified: true,
  },
  {
    id: '3',
    title: 'Сертификат качества',
    issuedDate: new Date('2021-10-01'),
    issuerId: 'EFQM',
    verified: false,
    image: {
      url: 'https://static.vecteezy.com/system/resources/previews/000/636/160/original/portrait-luxury-certificate-template-with-elegant-border-frame-diploma-design-for-graduation-or-completion-vector.jpg',
    },
  },
  {
    id: '4',
    title: 'Сертификат об образовании',
    issuedDate: new Date('2020-06-30'),
    issuerId: 'Университет',
    verified: true,
    image: {
      url: 'https://www.mos-elektrika.ru/wp-content/uploads/2016/last_certificate/sert4.jpg',
    },
  },
  {
    id: '5',
    title: 'Сертификат о прохождении курса',
    issuedDate: new Date('2019-04-12'),
    issuerId: 'Платформа онлайн-обучения',
    verified: false,
  },
]

const certificates = ref(mockCertificates)

const mockIssuers: Issuer[] = [
  { id: 'hnhfa36gfd', name: 'Google', certificateTypes: ['Security Certificate', 'Compliance Certificate'] },
  { id: 'ap4vteple', name: 'Apple', certificateTypes: ['Quality Certificate'] },
  { id: 'micsww3rosoft', name: 'Microsoft', certificateTypes: ['Training Certificate', 'Educational Certificate'] },
  { id: 'am24a5zon', name: 'Amazon', certificateTypes: ['Educational Certificate'] },
  { id: 'm2cse762ta', name: 'Meta', certificateTypes: ['Training Certificate'] },
]
const issuers = ref(mockIssuers)

const { toast } = useToast()
const df = new DateFormatter('ru-RU', { dateStyle: 'short' })

type Issuer = {
  id: string
  name: string
  certificateTypes: string[]
}

const requiredError = 'Это обязательное поле'

const requestSchema = z.object({
  issuerId: z.string({ message: requiredError }),
  certificateType: z.string({ required_error: requiredError }),
  message: z.string().max(256).optional(),
})

const isRequestDialogOpen = ref(false)

const getIssuerById = (id: string): Issuer | undefined => {
  return issuers.value.find(issuer => issuer.id === id)
}
// const { values: requestValues, handleSubmit: handleSubmitRequest, setFieldValue: setRequestFieldValue, meta: requestMeta }
//  = useForm({ validationSchema: toTypedSchema(requestSchema), initialValues: {} })

// const onSubmitRequest = handleSubmitRequest((_values) => {
//   toast({ title: 'Запрос отправлен', variant: 'default', duration: 1000 })
//   isRequestDialogOpen.value = false
// })

const onSubmitRequest = (_values: z.infer<typeof requestSchema>): void => {
  toast({ title: 'Запрос отправлен', variant: 'default', duration: 1000 })
  isRequestDialogOpen.value = false
}

const createSchema = z.object({
  title: z.string({ message: requiredError }).min(1).max(256),
})

const isCreateDialogOpen = ref(false)
const { values: _createValues, handleSubmit: handleSubmitCreate, meta: createMeta }
= useForm({ validationSchema: toTypedSchema(createSchema), initialValues: {} })

const onSubmitCreate = handleSubmitCreate((values) => {
  const certificate: Certificate = {
    id: crypto.randomUUID(),
    issuedDate: new Date(),
    issuerId: 'me',
    title: values.title,
    verified: false,
  }
  certificates.value.splice(0, 0, certificate)
  isCreateDialogOpen.value = false
})
</script>

<template>
  <div class="flex gap-y-6 flex-col mt-6 mb-8 mx-8">
    <h1 class="text-4xl font-bold">
      Сертификаты
    </h1>
    <div class="flex gap-x-4">
      <Form
        v-slot="{ handleSubmit, meta: requestMeta, values: requestValues, setFieldValue: setRequestFieldValue, resetForm: resetRequestForm }"
        as=""
        :validation-schema="toTypedSchema(requestSchema)"
      >
        <Dialog
          :model-value:open="isRequestDialogOpen"
          @update:open="(isOpen) => {
            isRequestDialogOpen = isOpen
            if (!isOpen) resetRequestForm()
          }"
        >
          <DialogTrigger :as-child="true">
            <Button>
              Запросить
            </Button>
          </DialogTrigger>
          <DialogContent>
            <DialogHeader>
              <DialogTitle>Запросить сертификат</DialogTitle>
              <VisuallyHidden>
                <DialogDescription>Запросить сертификат</DialogDescription>
              </VisuallyHidden>
            </DialogHeader>
            <form
              class="flex flex-col gap-y-4"
              @submit.prevent="handleSubmit(onSubmitRequest as any)"
            >
              <FormField
                name="issuerId"
              >
                <FormItem>
                  <FormLabel>Выберете эмитента</FormLabel>
                  <Select
                    :model-value="requestValues.issuerId"
                    @update:model-value="(id) => {
                      setRequestFieldValue('issuerId', id)
                      setRequestFieldValue('certificateType', undefined, false)
                    }"
                  >
                    <SelectTrigger>
                      <FormControl>
                        <SelectValue />
                      </FormControl>
                      <SelectContent>
                        <SelectItem
                          v-for="issuer in issuers"
                          :key="issuer.id"
                          :value="issuer.id"
                        >
                          {{ issuer.name }} <span class="ml-2 text-sm text-slate-400">{{ issuer.id }}</span>
                        </SelectItem>
                      </SelectContent>
                    </SelectTrigger>
                  </Select>
                  <FormMessage />
                </FormItem>
              </FormField>

              <FormField
                v-slot="{ componentField }"
                name="certificateType"
              >
                <FormItem v-if="requestValues.issuerId">
                  <FormLabel>Выберете тип сертификата</FormLabel>
                  <Select v-bind="componentField">
                    <SelectTrigger>
                      <FormControl>
                        <SelectValue />
                      </FormControl>
                      <SelectContent>
                        <SelectItem
                          v-for="type in requestValues.issuerId ? getIssuerById(requestValues.issuerId)?.certificateTypes ?? [] : []"
                          :key="type"
                          :value="type"
                        >
                          {{ type }}
                        </SelectItem>
                      </SelectContent>
                    </SelectTrigger>
                  </Select>
                  <FormMessage />
                </FormItem>
              </FormField>
              <FormField
                v-slot="{ componentField }"
                name="message"
              >
                <FormItem>
                  <FormLabel>Сообщение для эмитента</FormLabel>
                  <FormControl>
                    <Input v-bind="componentField" />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              </FormField>
              <Button
                :disabled="!(requestMeta.dirty && requestMeta.valid)"
                type="submit"
              >
                Отправить запрос
              </Button>
            </form>
          </DialogContent>
        </Dialog>
      </Form>

      <Dialog v-model:open="isCreateDialogOpen">
        <DialogTrigger :as-child="true">
          <Button
            type="submit"
            variant="secondary"
          >
            Создать неподтверждённый
          </Button>
        </DialogTrigger>
        <DialogContent>
          <DialogHeader>
            <DialogTitle>Создать неподтверждённый</DialogTitle>
            <VisuallyHidden>
              <DialogDescription>Создать неподтверждённый</DialogDescription>
            </VisuallyHidden>
          </DialogHeader>
          <form
            class="flex flex-col gap-y-4"
            @submit.prevent="onSubmitCreate"
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
            <Button
              :disabled="!(createMeta.dirty && createMeta.valid)"
              type="submit"
            >
              Добавить
            </Button>
          </form>
        </DialogContent>
      </Dialog>
    </div>
    <div class="grid grid-cols-[repeat(auto-fill,minmax(320px,1fr))] gap-4">
      <div
        v-for="certificate in certificates"
        :key="certificate.id"
        class="flex flex-col p-4 bg-slate-50 rounded-lg"
      >
        <div class="">
          <Dialog v-if="certificate.image">
            <DialogTrigger>
              <img

                :alt="certificate.title"
                class="bg-slate-200 w-full object-cover aspect-video rounded-lg"
                :src="certificate.image?.url"
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
          >
            <ShieldCheck
              v-if="certificate.verified"
              class="text-slate-500"
              :size="100"
              :stroke-width="0.7"
            />
            <Shield
              v-else
              class="text-slate-500"
              :size="100"
              :stroke-width="0.7"
            />
          </div>
        </div>
        <span class="font-semibold mt-4">{{ certificate.title }}</span>
        <div class="flex justify-between">
          <span class="font-light">{{ df.format(certificate.issuedDate) }}</span>
          <TooltipProvider v-if="certificate.verified">
            <Tooltip :delay-duration="300">
              <TooltipTrigger>
                <Check
                  class="w-6 h-6 text-green-500"
                  :stroke-width="2"
                />
              </TooltipTrigger>
              <TooltipContent class="flex gap-2">
                <span>Подтверждено эмитентом</span>
                <span class="font-semibold">{{ certificate.issuerId }}</span>
              </TooltipContent>
            </Tooltip>
          </TooltipProvider>
        </div>
      </div>
    </div>
  </div>
</template>
