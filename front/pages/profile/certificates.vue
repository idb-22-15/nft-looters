<script setup lang="ts">
import { DateFormatter } from '@internationalized/date'
import { Check } from 'lucide-vue-next'
import { Tooltip, TooltipProvider, TooltipContent, TooltipTrigger } from '~/components/ui/tooltip'
import { Dialog, DialogContent, DialogHeader, DialogTitle, DialogDescription, DialogTrigger } from '~/components/ui/dialog'
import { FormField, FormItem, FormLabel, FormControl, FormMessage } from '~/components/ui/form'
import { Button } from '~/components/ui/button'

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
}

const certificates: Certificate[] = [
  {
    id: '1',
    title: 'Сертификат безопасности',
    issuedDate: new Date('2023-03-08'),
    issuerId: 'ISACA',
    verified: true,
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
  },
  {
    id: '4',
    title: 'Сертификат об образовании',
    issuedDate: new Date('2020-06-30'),
    issuerId: 'Университет',
    verified: true,
  },
  {
    id: '5',
    title: 'Сертификат о прохождении курса',
    issuedDate: new Date('2019-04-12'),
    issuerId: 'Платформа онлайн-обучения',
    verified: false,
  },
]

const issuers: Issuer[] = [
  { id: 'hnhfa36gfd', name: 'Google', certificateTypes: ['Security Certificate', 'Compliance Certificate'] },
  { id: 'ap4vteple', name: 'Apple', certificateTypes: ['Quality Certificate'] },
  { id: 'micsww3rosoft', name: 'Microsoft', certificateTypes: ['Training Certificate', 'Educational Certificate'] },
  { id: 'am24a5zon', name: 'Amazon', certificateTypes: ['Educational Certificate'] },
  { id: 'm2cse762ta', name: 'Meta', certificateTypes: ['Training Certificate'] },
]

const { toast } = useToast()
const df = new DateFormatter('ru-RU', { dateStyle: 'short' })

type Issuer = {
  id: string
  name: string
  certificateTypes: string[]
}

const requestSchema = z.object({
  issuer: z.object({
    id: z.string(),
    name: z.string(),
    certificateTypes: z.array(z.string()),
  }),
  certificateType: z.string(),
  message: z.string().optional(),
})

const { values: requestValues, handleSubmit: handleSubmitRequest, setFieldValue: setRequestFieldValue, meta: requestMeta }
 = useForm({ validationSchema: toTypedSchema(requestSchema), initialValues: {} })

const onSubmitRequest = handleSubmitRequest((values) => {
  toast({ title: 'Запрос отправлен', variant: 'default', duration: 1000 })
  // const certificate: Certificate = {
  //   id: crypto.randomUUID(),
  //   issuedDate: new Date(),
  //   issuerId: 'me',
  //   title: values.
  // }
})
</script>

<template>
  <div class="flex gap-y-6 flex-col mt-6 mb-8 mx-8">
    <h1 class="text-4xl font-bold">
      Сертификаты
    </h1>
    <div class="flex gap-x-4">
      <Dialog>
        <DialogTrigger>
          <Button>
            Запросить
          </Button>
        </DialogTrigger>
        <DialogContent>
          <DialogHeader>
            <DialogTitle>Запросить сертификат</DialogTitle>
          </DialogHeader>
          <form
            class="flex flex-col gap-y-4"
            @submit.prevent="onSubmitRequest"
          >
            <FormField
              v-slot="{ componentField }"
              name="issuer"
            >
              <FormItem>
                <FormLabel>Выберете эмитента</FormLabel>
                <Select
                  :model-value="requestValues.issuer?.id"
                  @update:model-value="(id) => {
                    const issuer = issuers.find(is => is.id === id)
                    setRequestFieldValue('issuer', issuer)
                    setRequestFieldValue('issuer.id', issuer?.id)
                    setRequestFieldValue('issuer.name', issuer?.name)
                    setRequestFieldValue('issuer.certificateTypes', issuer?.certificateTypes)

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
              </FormItem>
              <FormMessage />
            </FormField>
            <FormField
              v-if="requestValues.issuer?.certificateTypes"
              v-slot="{ componentField }"
              name="certificateType"
            >
              <FormItem>
                <FormLabel>Выберете тип сертификата</FormLabel>
                <Select v-bind="componentField">
                  <SelectTrigger>
                    <FormControl>
                      <SelectValue />
                    </FormControl>
                    <SelectContent>
                      <SelectItem
                        v-for="type in requestValues.issuer.certificateTypes"
                        :key="type"
                        :value="type"
                      >
                        {{ type }}
                      </SelectItem>
                    </SelectContent>
                  </SelectTrigger>
                </Select>
              </FormItem>
              <FormMessage />
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
              </FormItem>
              <FormMessage />
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

      <Dialog>
        <DialogTrigger>
          <Button
            type="submit"
            variant="secondary"
          >
            Создать от своего имени
          </Button>
        </DialogTrigger>
        <DialogContent>
          ff
        </DialogContent>
      </Dialog>
    </div>
    <div class="grid grid-cols-4 gap-4">
      <div
        v-for="certificate in certificates"
        :key="certificate.id"
        class="flex flex-col gap-y-2 p-4 bg-slate-50 rounded-lg"
      >
        <span>{{ certificate.title }}</span>
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
