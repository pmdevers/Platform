﻿using StreamWave;

namespace FinSecure.Platform.Hypotheek.Domain.Aanvragers.Events;

public record RechtspersoonCreated(AanvragerId AanvragerId) : Event;
