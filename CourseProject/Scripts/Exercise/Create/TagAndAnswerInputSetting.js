(function() {
  $(function() {
    return $(document).ready(function() {
      $('#inputTags').textext({
        plugins: 'tags autocomplete ajax prompt',
        prompt: 'Write here youre tags...',
        ajax: {
          url: '/Exercise/TagAutocompliteSearch',
          dataType: 'json',
          cacheResults: true
        }
      });
      alert("init");
      return $('#inputAnswers').textext({
        plugins: 'tags prompt',
        prompt: 'Write here right answers...'
      });
    });
  });

}).call(this);

//# sourceMappingURL=TagAndAnswerInputSetting.js.map
