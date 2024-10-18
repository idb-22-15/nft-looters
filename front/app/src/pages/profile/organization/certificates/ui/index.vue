<script setup lang="ts">
import { mockCertificates } from '../__mocks__'
import CertificateItem from './CertificateItem.vue'

const certificatesToConfirm = ref(mockCertificates)
const confirmedCertificates = ref(mockCertificates)

const confirmCertificate = (certificate: Certificate) => {
}
const revokeCertificate = (certificate: Certificate) => {
}
</script>

<template>
  <div class="flex gap-y-6 flex-col mt-6 mb-8 mx-8">
    <h1 class="text-4xl font-bold">
      Сертификаты
    </h1>

    <section>
      <h2 class="text-2xl font-semibold ">
        Запросы на подтверждение
      </h2>

      <div class="mt-3 grid grid-cols-[repeat(auto-fill,minmax(320px,1fr))] gap-4">
        <CertificateItem
          v-for="certificate in certificatesToConfirm"
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
        <CertificateItem
          v-for="certificate in certificatesToConfirm"
          :key="certificate.id"
          :certificate="certificate"
          @certificate-action="confirmCertificate"
        />
      </div>
    </section>
  </div>
</template>
