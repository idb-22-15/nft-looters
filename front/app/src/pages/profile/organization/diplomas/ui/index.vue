<script setup lang="ts">
import type { Diploma } from '../types'

import { mockCertificates } from '../__mocks__'
import DiplomaItem from './DiplomaItem.vue'

const diplomasToConfirm = ref(mockCertificates)
const confirmedDiplomas = ref(mockCertificates)

const confirmCertificate = (certificate: Diploma) => {
}
const revokeCertificate = (certificate: Diploma) => {
}
</script>

<template>
  <div class="flex gap-y-6 flex-col mt-6 mb-8 mx-8">
    <h1 class="text-4xl font-bold">
      Дипломы
    </h1>

    <section>
      <h2 class="text-2xl font-semibold ">
        Запросы на подтверждение
      </h2>

      <div class="mt-3 grid grid-cols-[repeat(auto-fill,minmax(320px,1fr))] gap-4">
        <DiplomaItem
          v-for="certificate in diplomasToConfirm"
          :key="certificate.id"
          :certificate="certificate"
          @certificate-action="confirmCertificate"
        />
      </div>
    </section>

    <section>
      <h2 class="text-2xl font-semibold ">
        Выданные сертификаты
      </h2>

      <div class="mt-3 grid grid-cols-[repeat(auto-fill,minmax(320px,1fr))] gap-4">
        <DiplomaItem
          v-for="certificate in diplomasToConfirm"
          :key="certificate.id"
          :certificate="certificate"
          @certificate-action="confirmCertificate"
        />
      </div>
    </section>
  </div>
</template>
