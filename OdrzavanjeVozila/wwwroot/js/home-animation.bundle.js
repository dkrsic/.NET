(() => {
  const container = document.querySelector('.container');
  const range = document.querySelector('.range');
  if (!container) return;

  const particleCount = 150;
  const particles = [];

  const rand = (min = 0, max = 1, dec = 0) => {
    const m = 10 ** dec;
    return Math.floor((Math.random() * (max - min + 1 / m) + min) * m) / m;
  };
  const lerp = (a, b, t) => a + (b - a) * t;
  const easeOutQuad = (t) => 1 - (1 - t) * (1 - t);

  for (let i = 0; i < particleCount; i++) {
    const el = document.createElement('div');
    el.classList.add('particle');
    container.appendChild(el);
    particles.push({
      el,
      start: performance.now() + rand(0, 1000),
      dur: rand(800, 3000),
      tx: rand(-10, 10, 2),
      ty: rand(-3, 3, 2)
    });
  }

  let fps = 60;
  let intervalId = null;

  function tick() {
    const now = performance.now();
    for (let p of particles) {
      let t = (now - p.start) / p.dur;
      if (t >= 1) {
        p.start = now + rand(0, 300);
        p.dur = rand(800, 3000);
        p.tx = rand(-10, 10, 2);
        p.ty = rand(-3, 3, 2);
        t = 0;
      }
      const prog = easeOutQuad(Math.min(1, Math.max(0, t)));
      const x = lerp(0, p.tx, prog);
      const y = lerp(0, p.ty, prog);
      const scale = 1 - Math.abs(0.5 - prog) * 2; // pop in/out
      p.el.style.transform = `translate(${x}rem, ${y}rem) scale(${scale})`;
      p.el.style.willChange = 'transform';
    }
  }

  function schedule() {
    if (intervalId) clearInterval(intervalId);
    intervalId = setInterval(tick, Math.max(1, Math.round(1000 / fps)));
  }

  schedule();

  if (range) {
    range.addEventListener('input', function () {
      const v = +this.value || 60;
      fps = Math.max(1, Math.round(v));
      schedule();
    });
  }
})();
