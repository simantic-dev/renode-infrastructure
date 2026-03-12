using System.Collections.Generic;

using Antmicro.Renode.Core;
using Antmicro.Renode.Core.Structure.Registers;
using Antmicro.Renode.Peripherals.Bus;
using Antmicro.Renode.Peripherals.Timers;

namespace Antmicro.Renode.Peripherals.Miscellaneous
{
    [AllowedTranslations(AllowedTranslation.ByteToDoubleWord | AllowedTranslation.WordToDoubleWord)]
    public sealed class STM32F0_RCC : IDoubleWordPeripheral, IKnownSize, IProvidesRegisterCollection<DoubleWordRegisterCollection>
    {
        public STM32F0_RCC(IMachine machine)
        {
            var registersMap = new Dictionary<long, DoubleWordRegister>
            {
                {(long)Registers.RCC_CR, new DoubleWordRegister(this, 0x00000083) // 0x0000XX83
                    .WithFlag(0, out var hsion, name: "HSION")
                    .WithFlag(1, FieldMode.Read, valueProviderCallback: _ => hsion.Value, name: "HSIRDY")
                    .WithReservedBits(2, 1)
                    .WithValueField(3, 5, out var hsitrim, name: "HSITRIM")
                    .WithTag("HSICAL", 8, 8)
                    .WithFlag(16, out var hseon, name: "HSEON")
                    .WithFlag(17, FieldMode.Read, valueProviderCallback: _ => hseon.Value, name: "HSERDY")
                    .WithTag("HSEBYP", 18, 1)
                    .WithTag("CSSON", 19, 1)
                    .WithReservedBits(20, 4)
                    .WithFlag(24, out var pllon, name: "PLLON")
                    .WithFlag(25, FieldMode.Read, valueProviderCallback: _ => pllon.Value, name: "PLLRDY")
                    .WithReservedBits(26, 6)
                },
                {(long)Registers.RCC_CFGR, new DoubleWordRegister(this, 0x00000000)
                },
                {(long)Registers.RCC_CIR, new DoubleWordRegister(this, 0x00000000)
                },
                {(long)Registers.RCC_APB2RSTR, new DoubleWordRegister(this, 0x00000000)
                },
                {(long)Registers.RCC_APB1RSTR, new DoubleWordRegister(this, 0x00000000)
                },
                {(long)Registers.RCC_AHBENR, new DoubleWordRegister(this, 0x00000014)
                },
                {(long)Registers.RCC_APB2ENR, new DoubleWordRegister(this, 0x00000000)
                },
                {(long)Registers.RCC_APB1ENR, new DoubleWordRegister(this, 0x00000000)
                },
                {(long)Registers.RCC_BDCR, new DoubleWordRegister(this, 0x00000018)
                },
            };
            RegistersCollection = new DoubleWordRegisterCollection(this, registersMap);
        }

        public uint ReadDoubleWord(long offset)
        {
            return RegistersCollection.Read(offset);
        }

        public void WriteDoubleWord(long offset, uint value)
        {
            RegistersCollection.Write(offset, value);
        }

        public void Reset()
        {
            RegistersCollection.Reset();
        }

        public long Size => 0x400; // 1kB

        public DoubleWordRegisterCollection RegistersCollection { get; }
        
        private enum Registers
        {
            RCC_CR = 0x00,
            RCC_CFGR = 0x04,
            RCC_CIR = 0x08,
            RCC_APB2RSTR = 0x0C,
            RCC_APB1RSTR = 0x10,
            RCC_AHBENR = 0x14,
            RCC_APB2ENR = 0x18,
            RCC_APB1ENR = 0x1C,
            RCC_BDCR = 0x20,
            RCC_CSR = 0x24,
            RCC_AHBRSTR = 0x28,
            RCC_CFGR2 = 0x2C,
            RCC_CFGR3 = 0x30,
            RCC_CR2 = 0x34
        }
    }
}