<script setup lang="ts">
import { twMerge } from "tailwind-merge";
import {
  defaultActiveRingColorClasses,
  defaultRingColorClasses,
  generateColorClasses,
} from "../Styles/DefaultGenerators";

export type TextInputProps = {
  label?: string;
  placeholder?: string;
  disabled?: boolean;
  name?: string;
  id?: string;
  autocomplete?:
    | "off"
    | "name"
    | "email"
    | "username"
    | "new-password"
    | "current-password";
  minlength?: number;
  maxlength?: number;
  required?: boolean;
  validationError?: boolean;
  validationErrorMessage?: string;
  validationErrorMessageReserveSpace?: boolean;
  validationSuccess?: boolean;
  validationSuccessMessage?: string;
  validationSuccessMessageReserveSpace?: boolean;
};

const model = defineModel<string>();
const props = defineProps<TextInputProps>();
</script>

<template>
  <div class="flex flex-col">
    <label v-if="label" :for="id" class="mb-2 text-sm font-medium">
      {{ label }}
    </label>
    <input
      type="text"
      :class="[
        twMerge(
          'outline-0',
          'rounded-lg px-4 py-2',
          disabled ? 'cursor-not-allowed bg-zinc-50 opacity-75' : '',
          defaultRingColorClasses(),
          defaultActiveRingColorClasses(),
          validationError
            ? generateColorClasses(
                'error',
                disabled ? 'subtle' : 'ghost',
                false,
                false,
                true,
              )
            : validationSuccess
              ? generateColorClasses(
                  'success',
                  disabled ? 'subtle' : 'ghost',
                  false,
                  false,
                  true,
                )
              : '',
        ),
      ]"
      v-model="model"
      :placeholder="placeholder"
      :disabled="disabled"
      :name="name"
      :id="id"
      :autocomplete="autocomplete"
      :minlength="minlength"
      :maxlength="maxlength"
      :required="required"
    />
    <div
      v-if="validationError && validationErrorMessage"
      :class="[generateColorClasses('error', 'ghost'), 'pt-1 text-sm']"
    >
      {{ validationErrorMessage }}
    </div>
    <div
      v-else-if="validationSuccess && validationSuccessMessage"
      :class="[generateColorClasses('success', 'ghost'), 'pt-1 text-sm']"
    >
      {{ validationSuccessMessage }}
    </div>
  </div>
</template>
