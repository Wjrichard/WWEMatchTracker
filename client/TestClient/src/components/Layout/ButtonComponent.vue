<script setup lang="ts">
import { computed, type FunctionalComponent, useAttrs } from 'vue'
import Dots3xBounce from '@/components/Layout/Dots3xBounce.vue'

const props = defineProps<{
    /**
     * Default: `white`
     */
    color?: 'blue' | 'indigo' | 'gray' | 'green' | 'red' | 'yellow' | 'white'
    /**
     * Default: `normal`
     */
    size?: 'extra-small' | 'small' | 'normal' | 'large' | 'extra-large'
    /**
     * Default: `medium`
     */
    rounded?: 'small' | 'normal' | 'medium' | 'large' | 'extra-large' | 'full'
    bold?: boolean
    working?: boolean
    narrow?: boolean
    unbound?: boolean
    disabled?: boolean
    primary?: boolean
    belongsToInputGroup?: boolean
    icon?: FunctionalComponent
}>()

const computedStyles = computed(() => {
    let tailwindBaseColor: string
    let tailwindBgColor: string
    let tailwindRingColor: string
    let tailwindSize: string
    let tailwindIconSize: string
    let tailwindRounded: string
    const tailwindColorWeights = {
        bg: 100,
        bgDisabled: 100,
        bgHover: 50,
        border: 300,
        borderFocus: 500,
        text: 500,
        textDisabled: 300
    }

    switch (props.color) {
        case 'blue':
            tailwindBaseColor = 'blue'
            tailwindRingColor = 'blue'
            tailwindBgColor = 'blue'
            break
        case 'indigo':
            tailwindBaseColor = 'indigo'
            tailwindRingColor = 'indigo'
            tailwindBgColor = 'indigo'
            break
        case 'green':
            tailwindBaseColor = 'green'
            tailwindRingColor = 'green'
            tailwindBgColor = 'green'
            break
        case 'red':
            tailwindBaseColor = 'red'
            tailwindRingColor = 'red'
            tailwindBgColor = 'red'
            break
        case 'yellow':
            tailwindBaseColor = 'yellow'
            tailwindRingColor = 'yellow'
            tailwindBgColor = 'yellow'
            break

        case 'gray':
            tailwindBaseColor = 'slate'
            tailwindRingColor = 'indigo'
            tailwindBgColor = 'slate'
            break

        case 'white':
        default:
            tailwindBaseColor = 'gray'
            tailwindRingColor = 'gray'
            tailwindBgColor = 'white'
    }

    switch (props.size) {
        case 'extra-small':
            tailwindSize = 'text-xs'
            tailwindIconSize = 'h-4 w-4'
            break
        case 'small':
            tailwindSize = 'text-sm'
            tailwindIconSize = 'h-4 w-4'
            break
        case 'large':
            tailwindSize = 'text-lg'
            tailwindIconSize = 'h-6 w-6'
            break
        case 'extra-large':
            tailwindSize = 'text-xl'
            tailwindIconSize = 'h-6 w-6'
            break

        case 'normal':
        default:
            tailwindSize = 'text-base'
            tailwindIconSize = 'h-5 w-5'
    }

    switch (props.rounded) {
        case 'small':
            tailwindRounded = 'rounded-sm'
            break
        case 'normal':
            tailwindRounded = 'rounded'
            break
        case 'large':
            tailwindRounded = 'rounded-lg'
            break
        case 'extra-large':
            tailwindRounded = 'rounded-xl'
            break
        case 'full':
            tailwindRounded = 'rounded-full'
            break

        case 'medium':
        default:
            tailwindRounded = 'rounded-md'
    }

    if (props.belongsToInputGroup) {
        tailwindRounded += ' rounded-s-none'
    }
    if (props.primary) {
        if (props.unbound) {
            console.warn(
                '<Button unbound primary> - Combining unbound and primary is currently unsupported'
            )
        } else if (props.color === 'white') {
            console.warn(
                "<Button color='white' primary> - Combining color=white and primary is currently unsupported. Primary should only be used on buttons that perform primary actions like a Save modal confirm"
            )
        } else {
            tailwindColorWeights.bg = 500
            tailwindColorWeights.bgHover = 600
            tailwindColorWeights.text = 50
        }
    }

    let foundationClasses = `border focus:ring-1 px-${props.narrow ? '2' : '3'} py-${props.narrow ? '1' : '1.5'} ${tailwindSize} ${props.bold ? 'font-medium' : ''} ${tailwindRounded} focus:outline-none`
    let colorClasses
    const modifierColorClasses = `disabled:cursor-not-allowed disabled:text-${tailwindBaseColor}-${tailwindColorWeights.textDisabled} disabled:bg-${tailwindBaseColor}-${tailwindColorWeights.bgDisabled} disabled:opacity-70 focus:ring-${tailwindRingColor}-${tailwindColorWeights.border} focus:border-${tailwindRingColor}-${tailwindColorWeights.border}`

    if (props.unbound) {
        foundationClasses = `!border-0 ${foundationClasses}`
        colorClasses = `hover:text-${tailwindBaseColor}-${tailwindColorWeights.text}`
    } else {
        colorClasses = `text-${tailwindBaseColor}-${tailwindColorWeights.text} ${tailwindBgColor === 'white' ? 'bg-white' : `bg-${tailwindBgColor}-${tailwindColorWeights.bg}`} border-${tailwindBaseColor}-${tailwindColorWeights.border} hover:bg-${tailwindBaseColor}-${tailwindColorWeights.bgHover}`
    }

    const attrs = useAttrs()
    let useMinWidthFit = true
    if (attrs.class && typeof attrs.class === 'string' && attrs.class.includes('min-w-')) {
        // disables `min-w-fit` if a class like `min-w-32` is applied
        useMinWidthFit = false
    }

    return {
        Button: [foundationClasses, colorClasses, modifierColorClasses].join(' '),
        Icon: tailwindIconSize,
        Working: {
            Colors: {
                Bg:
                    tailwindBgColor === 'white'
                        ? 'bg-white fill-gray-500'
                        : `bg-${tailwindBgColor}-${props.primary ? 600 : 600}`
            }
        },
        UseMinWidthFit: useMinWidthFit
    }
})
</script>

<template>
    <button
        type="button"
        :disabled="disabled || working"
        class="relative flex items-center space-x-2 overflow-hidden whitespace-nowrap"
        :class="[
      computedStyles.Button,
      $slots.default ? '' : 'justify-center',
      computedStyles.UseMinWidthFit ? 'min-w-fit' : ''
    ]"
    >
    <span v-if="icon || $slots.icon" class="flex items-center" :class="computedStyles.Icon">
      <slot v-if="$slots.icon" name="icon" />
      <component :is="icon" name="icon" />
    </span>
        <span v-if="$slots.default" class="grow text-center"><slot /></span>
        <div
            v-if="working"
            class="absolute -left-2 top-0 flex h-full w-full"
            :class="[computedStyles.Working.Colors.Bg]"
        >
            <Dots3xBounce class="m-auto" />
        </div>
    </button>
</template>
